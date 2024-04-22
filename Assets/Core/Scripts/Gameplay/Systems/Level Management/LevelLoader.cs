using System;
using System.Collections;
using UnityEngine;
using IJunior.TypedScenes;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Level[] _levelsToLoad;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _waitingTime;

    private UnlockedLevelSaver _unlockedLevelSaver;

    private void Awake()
    {
        if (_levelsToLoad.Length < 1)
            throw new NullReferenceException($"No levels to load on {name}");

        _unlockedLevelSaver = new UnlockedLevelSaver();
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevelRoutine());
    }

    private IEnumerator LoadLevelRoutine()
    {
        _animator.SetTrigger("EndLevel");
        yield return new WaitForSecondsRealtime(_waitingTime);
        var lastLevel = GetLastUnlockLevel();
        Level_1.Load(lastLevel);
    }

    private Level GetLastUnlockLevel()
    {
        var levelIndex = _unlockedLevelSaver.GetLastUnlockLevelIndex();
        return _levelsToLoad[levelIndex];
    }
}
