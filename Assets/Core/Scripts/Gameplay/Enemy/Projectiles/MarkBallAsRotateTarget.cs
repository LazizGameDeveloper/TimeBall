using System;
using UnityEngine;

[RequireComponent(typeof(RotateToTarget))]
public class MarkBallAsRotateTarget : MonoBehaviour
{
    private static Transform Target
    {
        get
        {
            if (_target == null)
                _target = FindObjectOfType<BallController>().transform;
            return _target;
        }
    }

    private static Transform _target;
    
    private void Start()
    {
        var rotateToTarget = GetComponent<RotateToTarget>();
        rotateToTarget.Target = Target;
    }
}
