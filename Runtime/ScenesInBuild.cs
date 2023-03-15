using System.Collections.Generic;
using UnityEngine;

namespace Gilzoide.SceneReference
{
    public class ScenesInBuild : ScriptableObject
    {
        [SerializeField] private SceneReference[] _sceneReferences;

        public SceneReference[] SceneReferences => _sceneReferences;

#if UNITY_EDITOR
        public void Setup(List<SceneReference> scenes)
        {
            _sceneReferences = scenes.ToArray();
        }
#endif
    }
}
