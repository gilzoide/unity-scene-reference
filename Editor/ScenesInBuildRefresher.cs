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
        
        [InitializeOnLoadMethod]
        private static void Initialize()
        {
            Refresh();
            EditorBuildSettings.sceneListChanged += Refresh;
        }

        private static AssetMoveResult OnWillMoveAsset(string sourcePath, string destinationPath)
        {
            if (AssetDatabase.GetMainAssetTypeAtPath(sourcePath) == typeof(SceneAsset))
            {
                EditorCoroutineUtility.StartCoroutineOwnerless(RefreshNextFrame());
            }

            return AssetMoveResult.DidNotMove;
        }

        private static IEnumerator RefreshNextFrame()
        {
            yield return null;
            Refresh();
        }
    }
}
