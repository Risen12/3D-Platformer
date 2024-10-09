using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Mover : MonoBehaviour
{
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _jumpSpeed;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _walkBackwardsSpeed;
    [SerializeField] private GroundVerifier _groundVerifier;

    private bool _isRunning;
    private Rigidbody _rigidbody;

    public event Action<bool> MoveStateChanged;
    public event Action<bool> RunStateChanged;
    public event Action Jumped;
    public event Action<bool> MoveBackwardsStateChanged;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Move(float axisValue)
    {
        if (axisValue > 0)
        {
            ChangeMoveState(true);

            if (_isRunning)
            {
                transform.Translate(Vector3.forward * _runSpeed * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector3.forward * _walkSpeed * Time.deltaTime);
            }
        }
        else
        {
            ChangeMoveState(true, true);

            transform.Translate(Vector3.back * _walkBackwardsSpeed * Time.deltaTime);
        }
    }

    public void ChangeRunState(bool state)
    {
        _isRunning = state;
        RunStateChanged?.Invoke(state);
    }

    public void Jump()
    {
        if (_groundVerifier.IsGrounded)
        {
            _rigidbody.velocity = Vector3.up * _jumpSpeed;
            Jumped?.Invoke();
        }
    }

    public void Stop()
    {
        ChangeRunState(false);
        ChangeMoveState(false);
        ChangeMoveState(false, true);
    }

    public void Rotate(float rotation)
    {
        transform.Rotate(rotation * _rotateSpeed * Time.deltaTime * Vector3.up);
    }

    private void ChangeMoveState(bool state, bool isMoveBackwards = false)
    {
        if (isMoveBackwards)
        {
            MoveBackwardsStateChanged?.Invoke(state);
        }
        else
        {
            MoveStateChanged?.Invoke(state);
        }
    }
}
