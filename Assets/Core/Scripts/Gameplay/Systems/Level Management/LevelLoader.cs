using System;
using UnityEngine;
using IJunior.TypedScenes;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader Instance => _instance;
    private static LevelLoader _instance;
    
    [SerializeField] private Level[] _levelsToLoad;
    
    private UnlockedLevelSaver _unlockedLevelSaver;

    private void Awake()
    {
        if (_instance != null)
            Destroy(gameObject);
        
        _instance = this;
        DontDestroyOnLoad(gameObject);
        Initialize();
    }

    private void Initialize()
    {
        if (_levelsToLoad.Length < 1)
            throw new NullReferenceException($"No levels to load on {name}");

        _unlockedLevelSaver = new UnlockedLevelSaver();
    }

    public void LoadNextLevel()
    {
        var lastLevel = GetNextLevel();
        Level_1.Load(lastLevel);
    }

    private Level GetNextLevel()
    {
        var levelIndex = _unlockedLevelSaver.GetLastUnlockLevelIndex();
        return _levelsToLoad[levelIndex+1];
    }
}
