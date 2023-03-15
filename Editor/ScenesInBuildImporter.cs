using System.Collections.Generic;
using UnityEditor;
#if UNITY_2020_2_OR_NEWER
using UnityEditor.AssetImporters;
#else
using UnityEditor.Experimental.AssetImporters;
#endif
using UnityEngine;

namespace Gilzoide.SceneReference.Editor
{
    [ScriptedImporter(0, "scenesinbuild")]
    public class ScenesInBuildImporter : ScriptedImporter
    {
        public override void OnImportAsset(AssetImportContext ctx)
        {
            var sceneReferences = new List<SceneReference>();
            foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
            {
                if (!scene.enabled)
                {
                    continue;
                }

                var asset = AssetDatabase.LoadAssetAtPath<SceneAsset>(scene.path);
                if (asset == null)
                {
                    continue;
                }

                var sceneReference = ScriptableObject.CreateInstance<SceneReference>();
                sceneReference.Setup(asset);
                ctx.AddObjectToAsset(scene.guid.ToString(), sceneReference);

                sceneReferences.Add(sceneReference);
            }

            var scenesInBuild = ScriptableObject.CreateInstance<ScenesInBuild>();
            scenesInBuild.Setup(sceneReferences);
            ctx.AddObjectToAsset("main", scenesInBuild);
            ctx.SetMainObject(scenesInBuild);
        }
    }
}
