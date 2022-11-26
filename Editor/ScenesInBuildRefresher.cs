using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.EditorCoroutines.Editor;
using UnityEditor;

namespace Gilzoide.SceneReference.Editor
{
    public class ScenesInBuildRefresher : UnityEditor.AssetModificationProcessor
    {
        public static void Refresh()
        {
            IEnumerable<string> reimportPaths = AssetDatabase
                .FindAssets($"t:{nameof(ScenesInBuild)}")
                .Select(AssetDatabase.GUIDToAssetPath);

            foreach (string path in reimportPaths)
            {
                AssetDatabase.ImportAsset(path);
            }
        }

        public static void RefreshNextFrame()
        {
            EditorCoroutineUtility.StartCoroutineOwnerless(RunNextFrame(Refresh));
        }
        
        [InitializeOnLoadMethod]
        private static void Initialize()
        {
            // skipping a frame is important to avoid crash in Unity 2019
            RefreshNextFrame();
            EditorBuildSettings.sceneListChanged += Refresh;
        }

        private static AssetMoveResult OnWillMoveAsset(string sourcePath, string destinationPath)
        {
            if (AssetDatabase.GetMainAssetTypeAtPath(sourcePath) == typeof(SceneAsset))
            {
                RefreshNextFrame();
            }

            return AssetMoveResult.DidNotMove;
        }

        private static IEnumerator RunNextFrame(Action action)
        {
            yield return null;
            action?.Invoke();
        }
    }
}
