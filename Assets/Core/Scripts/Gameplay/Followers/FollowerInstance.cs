using UnityEngine;

public class FollowerInstant : MonoBehaviour
{
    [SerializeField] private Transform _target;

    void LateUpdate()
    {
        Move();
    }

    public void Move()
    {
        transform.position = _target.position;
    }
}
