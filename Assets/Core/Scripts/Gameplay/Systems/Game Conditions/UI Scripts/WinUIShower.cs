using UnityEngine;

public class WinUIShower : MonoBehaviour
{
    private Win _win;
    private bool _isInitialized = false;

    private void OnEnable()
    {
        if (_isInitialized)
            Subscribe();
    }

    private void OnDisable() 
    {
        if (_isInitialized)
            Unsubscribe();
    }

    public void Initialize(Win win)
    {
        _win = win;
        _isInitialized = true;
        Subscribe();
    }
    private void ShowUI()
    {
        gameObject.SetActive(true);
    }

    private void Subscribe() =>
        _win.OnVictoryEvent += ShowUI;

    private void Unsubscribe() =>
        _win.OnVictoryEvent -= ShowUI;
}
