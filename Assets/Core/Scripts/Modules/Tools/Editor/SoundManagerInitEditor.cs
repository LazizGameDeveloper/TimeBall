using System.Collections.Generic;
using System.IO;
using System.Text;
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
            CreateSoundsNamesConstants("AllSfxSounds", _script.FxGroup.Data);
            CreateSoundsNamesConstants("AllUiSounds", _script.UiGroup.Data);
            CreateSoundsNamesConstants("AllMusics", _script.MusicGroup.Data);
            _script.CreateMusicsSource();
            _script.CreateFXSoundsSource();
            _script.CreateUISoundsSource();
        }

        private void CreateSoundsNamesConstants(string fileName, IEnumerable<SoundData> sounds)
        {
            fileName = fileName.Replace(" ", "");
            var directoryPath = Application.dataPath + "/Core/Scripts/Modules/Audio/Constants/";
            var filePath = directoryPath + fileName + ".cs";

            var content = GetSoundsNameAsClass(fileName, sounds);

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                var buffer = Encoding.Default.GetBytes(content);
                stream.Write(buffer, 0, buffer.Length);
            }
            
            AssetDatabase.Refresh();
        }

        private string GetSoundsNameAsClass(string className, IEnumerable<SoundData> soundsData)
        {
            var content = "public static class " + className + "\n{\n";
            foreach (var field in soundsData)
            {
                var formatName = field.Name.Replace(" ", "_");
                content += $"\tpublic static readonly string {formatName} = \"{field.Name}\";\n";
            }
            content += "}";
            return content;
        }
    }
}