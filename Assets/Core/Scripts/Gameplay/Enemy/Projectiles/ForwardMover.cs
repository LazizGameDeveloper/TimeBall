using PrimeTween;
using UnityEngine;

public class ForwardMover : MonoBehaviour
{
    public float Speed;
    public float Acceleration;
    public float AccelerationDelay;
    public float AccelerationDuration;

    private float _currentSpeed;
    private Tween _tween;

    private void OnEnable()
    {
        _currentSpeed = Speed;
        _tween = Tween.Delay(
            AccelerationDelay,
            () => Tween.Custom(
                Speed, 
                Speed + Acceleration, 
                AccelerationDuration, 
                newValue => _currentSpeed = newValue));
    }

    private void OnDisable()
    {
        _tween.Complete();
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * (_currentSpeed * Time.deltaTime));
    }
}