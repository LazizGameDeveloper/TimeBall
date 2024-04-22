using UnityEngine;

public class Level : MonoBehaviour 
{
    public int LevelIndex => _levelIndex;

    [SerializeField] private int _levelIndex;
}
