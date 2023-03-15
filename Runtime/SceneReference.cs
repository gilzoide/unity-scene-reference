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

        /// <summary>Scene asset's name, as per <see cref="SceneAsset.name"/>.</summary>
        public string Name => _name;
        
        /// <summary>Scene asset's path, found using <see cref="AssetDatabase.GetAssetPath"/>.</summary>
        public string Path => _path;
        
        /// <summary>Scene's build index, found using <see cref="SceneUtility.GetBuildIndexByScenePath"/>.</summary>
        /// <remarks>Only Scenes enabled in Build Settings are generated, so this is always a non-negative number.</remarks>
        public int BuildIndex => _buildIndex;

#if UNITY_EDITOR
        public void Setup(SceneAsset asset)
        {
            _name = name = asset.name;
            _path = AssetDatabase.GetAssetPath(asset);
            _buildIndex = SceneUtility.GetBuildIndexByScenePath(_path);
        }
#endif

        /// <summary>Returns the corresponding <see cref="Scene"/> using <see cref="SceneManager.GetSceneByBuildIndex"/>.</summary>
        public Scene GetScene()
        {
            return SceneManager.GetSceneByBuildIndex(_buildIndex);
        }

        /// <summary>Loads the corresponding Scene using <see cref="SceneManager.LoadScene"/>.</summary>
        public void Load(LoadSceneMode mode = LoadSceneMode.Single)
        {
            SceneManager.LoadScene(_buildIndex, mode);
        }

        /// <summary>Loads the corresponding Scene asynchronously using <see cref="SceneManager.LoadSceneAsync"/>.</summary>
        public AsyncOperation LoadAsync(LoadSceneMode mode = LoadSceneMode.Single)
        {
            return SceneManager.LoadSceneAsync(_buildIndex, mode);
        }

#if UNITY_2018_3_OR_NEWER
        /// <summary>Loads the corresponding Scene asynchronously using <see cref="SceneManager.LoadSceneAsync"/>.</summary>
        public AsyncOperation LoadAsync(LoadSceneParameters parameters)
        {
            return SceneManager.LoadSceneAsync(_buildIndex, parameters);
        }
#endif

        /// <summary>Unloads the corresponding Scene asynchronously using <see cref="SceneManager.UnloadSceneAsync"/>.</summary>
        public AsyncOperation UnloadAsync()
        {
            return SceneManager.UnloadSceneAsync(_buildIndex);
        }

#if UNITY_2018_3_OR_NEWER
        /// <summary>Unloads the corresponding Scene asynchronously using <see cref="SceneManager.UnloadSceneAsync"/>.</summary>
        public AsyncOperation UnloadAsync(UnloadSceneOptions options)
        {
            return SceneManager.UnloadSceneAsync(_buildIndex, options);
        }
#endif
    }
}
