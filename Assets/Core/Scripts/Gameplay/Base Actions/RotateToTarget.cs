using System;
using UnityEngine;

public class RotateToTarget : MonoBehaviour
{
    public Transform Target
    {
        get => _target;
        set => _target = value;
    }
    [SerializeField] private Transform _target;
    [SerializeField] private float _rotationSpeed;

    public void Update()
    {
        LookAtTarget(_target);
    }

    private void LookAtTarget(Transform target)
    {
        var direction = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, direction, _rotationSpeed * Time.deltaTime);
    }
}
