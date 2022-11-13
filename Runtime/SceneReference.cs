using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gilzoide.SceneReference
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

        public Scene GetScene()
        {
            return SceneManager.GetSceneByBuildIndex(_buildIndex);
        }

        public void Load(LoadSceneMode mode = LoadSceneMode.Single)
        {
            SceneManager.LoadScene(_buildIndex, mode);
        }

        public AsyncOperation LoadAsync(LoadSceneMode mode = LoadSceneMode.Single)
        {
            return SceneManager.LoadSceneAsync(_buildIndex, mode);
        }

#if UNITY_2018_3_OR_NEWER
        public AsyncOperation LoadAsync(LoadSceneParameters parameters)
        {
            return SceneManager.LoadSceneAsync(_buildIndex, parameters);
        }
#endif

        public AsyncOperation UnloadAsync()
        {
            return SceneManager.UnloadSceneAsync(_buildIndex);
        }

#if UNITY_2018_3_OR_NEWER
        public AsyncOperation UnloadAsync(UnloadSceneOptions options)
        {
            return SceneManager.UnloadSceneAsync(_buildIndex, options);
        }
#endif
    }
}
