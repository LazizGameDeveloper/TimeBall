using GigaCreation.Tools.Service;
using PoolSystem.Main;
using PoolSystem.Service;
using UnityEngine;

public class LevelSwitcher : MonoBehaviour
{
    [SerializeField] private Win _win;

    private bool _hasWon;

    private void Update()
    {
        if (!_hasWon)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            ServiceLocator.Unregister(ServiceLocator.Get<PoolService<PoolObject>>());
            LevelLoader.Instance.LoadNextLevel();
        }
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