using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNumberManager : MonoBehaviour
{
    public Action OnNoEnemyLeftEvent;

    private int _enemiesLeft;
    private List<HealthComponent> _enemies = new();

    public void Initialize()
    {
        var enemies = FindObjectsOfType<EnemyTag>(true);
        foreach (var enemy in enemies)
        {
            if (enemy.TryGetComponent(out HealthComponent health))
            {
                _enemies.Add(health);
            }
        }

        if (_enemies.Count == 0)
            throw new NullReferenceException("There is no enemy on level");
        
        ResetEnemies();
    }

    public void ResetEnemies()
    {       
        foreach (var enemy in _enemies)
            enemy.gameObject.SetActive(true);

        _enemiesLeft = _enemies.Count;
        UnsubscribeOnDeathEvent();
        SubscribeOnDeathEvent();
    }

    private void OnEnemyDie()
    {
        _enemiesLeft--;
        if (_enemiesLeft < 1)
            OnNoEnemyLeftEvent?.Invoke();
    }

    private void SubscribeOnDeathEvent()
    {
        foreach (var enemy in _enemies)
            enemy.OnDeath += OnEnemyDie;
    }

    private void UnsubscribeOnDeathEvent()
    {
        foreach (var enemy in _enemies)
            enemy.OnDeath -= OnEnemyDie;
    }
}