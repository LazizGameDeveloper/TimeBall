using System;
using PrimeTween;
using UnityEngine;

public class RotateTowardsMovement : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;

    private Rigidbody _rb;
    private bool _isDirectionNormilized = true;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // if (!_isDirectionNormilized) return;
        
        var velocity = _rb.velocity;
        if (velocity.magnitude > 0.1f)
        {
            var targetRotation = Quaternion.LookRotation(_rb.velocity, Vector3.up);
            var finalRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, targetRotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            var angleY = finalRotation.eulerAngles.y;
            var rotation = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(rotation.x, angleY, rotation.z);
        }
    }

    // private void OnCollisionEnter(Collision other)
    // {
    //     _isDirectionNormilized = false;
    //     Tween.LocalRotation(transform, finalRotation, 0.25f).OnComplete(() => _isDirectionNormilized = true);
    // }
}