#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class WallMagnet : MonoBehaviour
{
    private void OnEnable()
    {
        SceneView.duringSceneGui += OnSceneGUI;
    }

    private void OnDisable()
    {
        SceneView.duringSceneGui -= OnSceneGUI;
    }

    private void OnSceneGUI(SceneView sceneView)
    {
        if (Selection.Contains(gameObject))
        {
            var e = Event.current;
            if (e.type == EventType.KeyDown)
            {
                if (e.keyCode == KeyCode.A)
                {
                    transform.Rotate(Vector3.up, -90, Space.World);
                    e.Use();
                }
                else if (e.keyCode == KeyCode.D)
                {
                    transform.Rotate(Vector3.up, 90, Space.World);
                    e.Use(); 
                }
            }
        }
    }
}
#endif