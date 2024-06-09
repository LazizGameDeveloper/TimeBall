using System;
using UnityEngine;

public abstract class Follower : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _speed;

    public void Move(float deltaTime)
    {
        var nextPosition = _target.position + _offset;
        transform.position = nextPosition;
    }

    [ContextMenu("SetOffsetAsPosition")]
    public void SetOffsetAsPosition()
    {
        if (_target == null)
            throw new NullReferenceException("Target must be assigned first!");

        _offset = transform.position - _target.position;
    }
    
    [ContextMenu("SetPositionAsOffset")]
    public void SetPositionAsOffset()
    {
        if (_target == null)
            throw new NullReferenceException("Target must be assigned first!");

        transform.position = _target.position + _offset;
    }
}
