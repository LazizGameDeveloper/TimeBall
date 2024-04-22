using UnityEngine;

public class TragectoryLineRenderer : MonoBehaviour
{
    [SerializeField] private LineRenderer _tragectoryLine;
    [SerializeField] private float _tragectoryLength;

    public bool IsActive => _tragectoryLine.gameObject.activeInHierarchy;

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
        _tragectoryLine.SetPosition(1, direction * _tragectoryLength);
    }
}
