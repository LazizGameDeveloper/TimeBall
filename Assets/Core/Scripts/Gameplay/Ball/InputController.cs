using UnityEngine;

[RequireComponent(typeof(IControllable))]
public class InputController: MonoBehaviour
{
    [SerializeField] private Joystick _joystick;
    public TimeManager TimeManager { get; private set; }

    private TragectoryLineRenderer _trajectoryLine;
    private IControllable _controllable;
    private Vector3 _direction;
    private Camera _mainCamera;

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
        TimeManager.UndoSlowmotion();
    }

    public void Initialize(TimeManager timeManager)
    {
        TimeManager = timeManager;
        _controllable = GetComponent<IControllable>();
        _trajectoryLine = GetComponent<TragectoryLineRenderer>();
        _mainCamera = Camera.main;
    }

    private void OnJoystickPointerDown()
    {
        TimeManager.DoSlowmotion();
        _trajectoryLine.Activate();
    }

    private void OnJoystickPointerDrag()
    {
        var joystickDirection = new Vector3(_joystick.Horizontal, 0, _joystick.Vertical);
        var worldDirection = _mainCamera.transform.TransformDirection(joystickDirection);
        _direction = worldDirection.AddY(-worldDirection.y).normalized;
        _trajectoryLine.SetDirection(_direction);
    }


    private void OnJoystickPointerUp()
    {
        _controllable.Move(_direction);
        TimeManager.UndoSlowmotion();
        _trajectoryLine.Deactivate();
    }
}
