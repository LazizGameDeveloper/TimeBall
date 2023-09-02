﻿using UnityEngine;
using LavkaRazrabotchika;

public class Bullet : PoolObject
{
    [SerializeField] private ParticleSystem _collisionEffect;
    [SerializeField] private float _speed;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        var cos = Mathf.Cos(transform.rotation.eulerAngles.y * Mathf.Deg2Rad);
        var sin = Mathf.Sin(transform.rotation.eulerAngles.y * Mathf.Deg2Rad);

        var direction = new Vector3(sin, 0, cos);

        _rigidbody.velocity = direction * _speed;
        Debug.Log(_rigidbody.velocity);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.TryGetComponent<BallContoller>(out var controller))
            controller.Die();

        Instantiate(_collisionEffect, transform.position, Quaternion.identity);

        gameObject.SetActive(false);
    }   
}