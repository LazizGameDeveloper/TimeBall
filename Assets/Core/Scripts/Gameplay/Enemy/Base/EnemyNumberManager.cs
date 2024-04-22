using System;
using UnityEngine;

public class EnemyNumberManager : MonoBehaviour
{
    public Action OnNoEnemyLeftEvent;

    private int _enemiesLeft;
    private EnemyBase[] _enemies;

    public void Initialize()
    {
        _enemies = FindObjectsOfType<EnemyBase>(true);
        ResetEnemies();
    }

    public void ResetEnemies()
    {       
        foreach (var enemy in _enemies)
            enemy.gameObject.SetActive(true);

        _enemiesLeft = _enemies.Length;
        UnsubscribeOnDeathEvent();
        SubscribeOnDeathEvent();
    }

    private void OnEnemyDie(EnemyBase enemy)
    {
        _enemiesLeft--;
        if (_enemiesLeft < 1)
            OnNoEnemyLeftEvent?.Invoke();
    }

    private void SubscribeOnDeathEvent()
    {
        foreach (var enemy in _enemies)
            enemy.OnEnemyDieEvent += OnEnemyDie;
    }

    private void UnsubscribeOnDeathEvent()
    {
        foreach (var enemy in _enemies)
            enemy.OnEnemyDieEvent -= OnEnemyDie;
    }
}