using System;
using UnityEngine;

public class GroundVerifier : MonoBehaviour
{
    private bool _isGrounded;

    public event Action<bool> GroundStateChanged;

    public bool IsGrounded => _isGrounded;

    private void OnTriggerEnter(Collider other)
    {
        ChangeGroundState(true);
    }

    private void OnTriggerExit(Collider other)
    {
        ChangeGroundState(false);
    }

    private void ChangeGroundState(bool state)
    {
        _isGrounded = state;
        GroundStateChanged?.Invoke(_isGrounded);
    }
}
