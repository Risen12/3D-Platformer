using UnityEngine;

[RequireComponent(typeof(Mover), typeof(Health))]
public class Player : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;

    private Mover _mover;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
    }

    private void OnEnable()
    {
        _inputReader.JumpButtonDown += OnJumpButtonDown;
    }

    private void OnDisable()
    {
        _inputReader.JumpButtonDown -= OnJumpButtonDown;
    }

    private void FixedUpdate()
    {
        if (_inputReader.VerticalDirection > 0)
        {
            if (_inputReader.IsRunning)
                _mover.ChangeRunState(true);
            else
                _mover.ChangeRunState(false);

            _mover.Move(_inputReader.VerticalDirection);
        }
        else if (_inputReader.VerticalDirection < 0)
        {
            _mover.Move(_inputReader.VerticalDirection);
        }
        else
        {
            _mover.Stop();
        }

        if (_inputReader.HorizontalMouseDirection != 0)
        {
            _mover.Rotate(_inputReader.HorizontalMouseDirection);
        }
    }

    public void TakeDamage(float damage)
    { 
        
    }

    private void OnJumpButtonDown()
    {
        _mover.Jump();
    }
}
