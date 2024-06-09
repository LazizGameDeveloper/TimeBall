using UnityEditor;
using UnityEngine;

public class QuickWallSnapper : EditorWindow
{
    private static Vector3[] _directions =
    {
        Vector3.back,
        Vector3.left,
        Vector3.forward,
        Vector3.right,
    };
    private static bool[] _hasWallsInDirection = new bool[4];

    [MenuItem("Snapping/RoundWallsPosition")]
    public static void RoundWallsPosition()
    {
        var containers = GameObject.FindGameObjectsWithTag("WallContainer");
        foreach (var container in containers)
        {
            foreach (var child in container.GetComponentsInChildren<Transform>())
            {
                child.transform.position = child.transform.position.RoundAll();
            }
        }
        Debug.Log("All walls placed!");
    }

    [MenuItem("Snapping/RotateHalfWallsCorrectly")]
    public static void RotateAllHalfWallsByAdjecent()
    {
        var containers = GameObject.FindGameObjectsWithTag("WallContainer");
        foreach (var container in containers)
        {
            foreach (var child in container.GetComponentsInChildren<WallMagnet>())
            {
                if (child == null) continue;

                for (var i = 0; i < _directions.Length; i++)
                {
                    var a = Physics.Raycast(child.transform.position, _directions[i], out _, 1f);
                    _hasWallsInDirection[i] = a;
                }

                if (_hasWallsInDirection[0] && _hasWallsInDirection[1])
                    child.transform.rotation = Quaternion.Euler(-90, 0, 0);
                else if (_hasWallsInDirection[1] && _hasWallsInDirection[2])
                    child.transform.rotation = Quaternion.Euler(-90, 0, 90);
                else if (_hasWallsInDirection[2] && _hasWallsInDirection[3])
                    child.transform.rotation = Quaternion.Euler(-90, 0, 180);
                else if (_hasWallsInDirection[3] && _hasWallsInDirection[0])
                    child.transform.rotation = Quaternion.Euler(-90, 0, 270);
                else
                    Debug.Log($"Didn't snapped! {child.name}");
                    
            }
        }
    }
}
