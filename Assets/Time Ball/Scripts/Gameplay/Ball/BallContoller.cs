using System;
using UnityEngine;

[SelectionBase]
[RequireComponent(typeof(Rigidbody))]
public class BallContoller : MonoBehaviour, IControllable
{
    public Action OnBallDeathEvent;
    [SerializeField] private float _speed;
    
    private Rigidbody _rigidbody;
    private Vector3 _direction;

    public void Update()
    {
        CorrectSpeed();
    }

    private void CorrectSpeed()
    {
        var velocity = _rigidbody.velocity; 
        var magnitude = velocity.magnitude;
        if (magnitude == 0) return;
        var multiplier = _speed / magnitude;
        _rigidbody.velocity = new Vector3(
            velocity.x * multiplier,
            velocity.y,
            velocity.z * multiplier
            );
    }

    public void Initialize()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 direction)
    {
        Throw(direction);
    }

    public void Die()
    {
        _rigidbody.velocity = Vector3.zero;
        OnBallDeathEvent?.Invoke();
        Time.timeScale = 1f;
        transform.gameObject.SetActive(false);
    }

    private void Throw(Vector3 direction)
    {
        var newVelocity = direction * _speed;

        if (newVelocity.x != 0 || newVelocity.z != 0)
            _rigidbody.velocity = newVelocity;
    }
}
