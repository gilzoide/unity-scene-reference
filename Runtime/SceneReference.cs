using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gilzoide.SceneReferences
{
    public class SceneReference : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private string _path;
        [SerializeField] private int _buildIndex;

        public string Name => _name;
        public string Path => _path;
        public int BuildIndex => _buildIndex;

#if UNITY_EDITOR
        public void Setup(SceneAsset asset)
        {
            _name = name = asset.name;
            _path = AssetDatabase.GetAssetPath(asset);
            _buildIndex = SceneUtility.GetBuildIndexByScenePath(_path);
        }
#endif
    }
}
