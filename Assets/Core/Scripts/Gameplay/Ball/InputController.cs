using UnityEngine;

[RequireComponent(typeof(IControllable))]
public class InputController: MonoBehaviour
{
    public TimeManager TimeManager => _timeManager;

    [SerializeField] private Joystick _joystick;

    private TragectoryLineRenderer _tragectoryLine;
    private IControllable _controllable;
    private TimeManager _timeManager;
    private Vector3 _direction;

    private void OnEnable()
    {
        _joystick.OnPoinerDownEvent += OnJoystickPointerDown;
        _joystick.OnPoinerDragEvent += OnJoystickPointerDrag;
        _joystick.OnPoinerUpEvent += OnJoystickPointerUp;
    }

    private void OnDisable()
    {
        _joystick.OnPoinerDownEvent -= OnJoystickPointerDown;
        _joystick.OnPoinerDragEvent -= OnJoystickPointerDrag;
        _joystick.OnPoinerUpEvent -= OnJoystickPointerUp;
        _timeManager.UndoSlowmotion();
    }

    public void Initialize(TimeManager timeManager)
    {
        _timeManager = timeManager;
        _controllable = GetComponent<IControllable>();
        _tragectoryLine = GetComponent<TragectoryLineRenderer>();
    }

    private void OnJoystickPointerDown()
    {
        _timeManager.DoSlowmotion();
        _tragectoryLine.Activate();
    }

    private void OnJoystickPointerDrag()
    {
        _direction = new Vector3(-_joystick.Vertical, 0, _joystick.Horizontal);
        _tragectoryLine.SetDirection(_direction.normalized);
    }

    private void OnJoystickPointerUp()
    {
        _controllable.Move(_direction);
        _timeManager.UndoSlowmotion();
        _tragectoryLine.Deactivate();
    }
}
