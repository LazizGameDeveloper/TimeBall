using UnityEngine;

public class UnlockedLevelSaver
{
    private const string UNLOCKED_KEY = "LastLevel";

    public void SaveLevelAsUnlocked(Level level)
    {
        PlayerPrefs.SetInt(UNLOCKED_KEY, level.LevelIndex);
    }

    public int GetLastUnlockLevelIndex()
    {
        var levelIndex = PlayerPrefs.GetInt(UNLOCKED_KEY, -1);
        return ++levelIndex;
    }
}