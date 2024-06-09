using System;
using UnityEngine;

public class TragectoryLineRenderer : MonoBehaviour
{
    [SerializeField] private LineRenderer _tragectoryLine;
    [SerializeField] private float _tragectoryLength;

    public bool IsActive => _tragectoryLine.gameObject.activeInHierarchy;
    private Vector3 _direction;

    private void Update()
    {
        DrawTrajectory();
    }

    private void OnDisable() => Deactivate();

    public void Initialize()
    {
        _tragectoryLine.SetPosition(0, Vector3.zero);
        _tragectoryLine.gameObject.SetActive(false);
    }

    public void Activate()
    {
        if (!IsActive)
            _tragectoryLine.gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        _tragectoryLine.gameObject.SetActive(false);
    }

    public void SetDirection(Vector3 direction)
    {
        _direction = direction;
    }

    private void DrawTrajectory()
    {
        var trajectoryLength = _tragectoryLength;
        if (Physics.Raycast(transform.position, _direction, out var hit, _tragectoryLength))
        {
            Debug.DrawLine(hit.point, hit.point + Vector3.up, Color.red);
            trajectoryLength = (hit.point - transform.position).magnitude + 1;
        }

        _tragectoryLine.SetPosition(1, _direction * trajectoryLength);
    }
}
