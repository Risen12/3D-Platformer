using System;
using TMPro;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const string VerticalAxisName = "Vertical";
    private const string HorizontalMouseAxisName = "Mouse X";
    private const string VerticalMouseAxisName = "Mouse Y";

    private float _verticalDirection;
    private float _horizontalMouseDirection;
    private float _verticalMouseDirection;
    private KeyCode _jumpButton;
    private KeyCode _runButton;
    private bool _isRunning;

    public event Action JumpButtonDown;

    public float VerticalDirection => _verticalDirection;
    public float HorizontalMouseDirection => _horizontalMouseDirection;
    public float VerticalMouseDirection => _verticalMouseDirection;
    public bool IsRunning => _isRunning;

    private void Awake()
    {
        _isRunning = false;
        _jumpButton = KeyCode.Space;
        _runButton = KeyCode.LeftShift;
    }

    private void Update()
    {
        _verticalDirection = Input.GetAxisRaw(VerticalAxisName);
        _horizontalMouseDirection = Input.GetAxis(HorizontalMouseAxisName);
        _verticalMouseDirection = Input.GetAxis(VerticalMouseAxisName);

        if (Input.GetKey(_runButton))
            _isRunning = true;
        else
            _isRunning = false;

        if (Input.GetKeyDown(_jumpButton))
            JumpButtonDown?.Invoke();
    }
}
