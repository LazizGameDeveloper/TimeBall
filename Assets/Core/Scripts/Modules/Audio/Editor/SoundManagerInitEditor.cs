using UnityEditor;
using UnityEngine;

namespace EditorExtensions
{
    [CustomEditor(typeof(SoundManagerInitializer))]
    public class SoundManagerInitEditor : Editor
    {
        private SoundManagerInitializer _script;

        private void OnEnable()
        {
            _script = (SoundManagerInitializer)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Create Missing Sounds"))
            {
                CreateSounds();
            }

            if (GUILayout.Button("Recreate all sound"))
            {
                var children = _script.gameObject.GetComponentsInChildren<Transform>();
                for (var index = children.Length-1; index > 0; index--)
                {
                    Undo.DestroyObjectImmediate(children[index].gameObject);
                }
                
                CreateSounds();
                Debug.Log("Sounds has been recreated!");
            }
            EditorGUILayout.EndHorizontal();
        }

        private void CreateSounds()
        {
            _script.CreateMusicsSource();
            _script.CreateFXSoundsSource();
            _script.CreateUISoundsSource();
        }
    }
}