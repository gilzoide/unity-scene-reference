using UnityEditor;

namespace Gilzoide.SceneReference.Editor
{
    [CustomEditor(typeof(SceneReference))]
    public class SceneReferenceEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            using (new EditorGUI.DisabledScope(true))
            {
                string path = serializedObject.FindProperty("_path").stringValue;
                EditorGUILayout.ObjectField(AssetDatabase.LoadAssetAtPath<SceneAsset>(path), typeof(SceneAsset), false);
            }
        }
    }
}
