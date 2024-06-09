using UnityEngine;

public abstract class AttackBase : MonoBehaviour
{
    [SerializeField] protected float AttackRate = 2;
    protected float PassedAttackTime;
    protected BarController ReloadBar;

    protected virtual void Awake()
    {
        ReloadBar = GetComponentInChildren<BarController>();
    }

    protected virtual void TryAttack()
    {
        PassedAttackTime += Time.deltaTime;
        if (PassedAttackTime >= AttackRate)
        {
            PassedAttackTime = 0;
            Attack();
        }
    }

    protected virtual void RefreshReloadBar()
    {
        ReloadBar.FillAmount = PassedAttackTime / AttackRate;
    }

    protected abstract void Attack();
}