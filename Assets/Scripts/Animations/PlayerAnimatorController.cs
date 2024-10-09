using UnityEngine;

[RequireComponent(typeof(Mover), typeof(Animator))]
public class PlayerAnimatorController : MonoBehaviour
{
    [SerializeField] private GroundVerifier _groundVerifier;

    private Mover _mover;
    private Animator _animator;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _mover.MoveStateChanged += OnMoveStateChanged;
        _mover.Jumped += OnJumped;
        _mover.RunStateChanged += OnRunStateChanged;
        _mover.MoveBackwardsStateChanged += OnMoveBackwardsStateChanged;

        _groundVerifier.GroundStateChanged += OnGroundStateChanged;
    }

    private void OnDisable()
    {
        _mover.MoveStateChanged -= OnMoveStateChanged;
        _mover.Jumped -= OnJumped;
        _mover.RunStateChanged -= OnRunStateChanged;
        _mover.MoveBackwardsStateChanged -= OnMoveBackwardsStateChanged;

        _groundVerifier.GroundStateChanged -= OnGroundStateChanged;
    }

    private void OnJumped()
    {
        _animator.SetTrigger(PlayerData.JumpTriggerName);
    }

    private void OnGroundStateChanged(bool state)
    {
        _animator.SetBool(PlayerData.IsGroundParameterName, state);
    }

    private void OnRunStateChanged(bool state)
    {
        _animator.SetBool(PlayerData.RunParameterName, state);
    }

    private void OnMoveStateChanged(bool state)
    {
        _animator.SetBool(PlayerData.WalkParameterName, state);
    }

    private void OnMoveBackwardsStateChanged(bool state)
    {
        _animator.SetBool(PlayerData.WalkBackwardsParameterName, state);
    }
}
