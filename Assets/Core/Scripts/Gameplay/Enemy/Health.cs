using UnityEngine;
using System;

public class HealthComponent : MonoBehaviour
{
    public Action OnDeath;
    [SerializeField] private int health = 3;

    public void ApplyDamage(int damage)
    {
        if (damage < 0)
            throw new ArgumentException("Damage can't be less than 0");

        health -= damage;
        if (health <= 0)
            OnDeath?.Invoke();
        
        gameObject.SetActive(false);
    }
}