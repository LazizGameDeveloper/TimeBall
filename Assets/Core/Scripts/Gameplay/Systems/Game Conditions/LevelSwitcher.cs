using UnityEngine;

public class LevelSwitcher : MonoBehaviour
{
    [SerializeField] private Win _win;
    [SerializeField] private LevelLoader _levelLoader;

    private bool _hasWon = false;

    private void Update()
    {
        if (!_hasWon)
            return;

        if (Input.GetMouseButtonDown(0))
            _levelLoader.LoadNextLevel();
    }

    private void OnEnable()
    {
        _win.OnVictoryEvent += OnVictory; 
    }

    private void OnDisable()
    {
        _win.OnVictoryEvent -= OnVictory;
    }

    private void OnVictory()
    {
        _hasWon = true;
    }
}