using UnityEngine;

public class WindTrap : MonoBehaviour
{
    [SerializeField] private float _windPower;
    [SerializeField] private Vector3 _windDirection;

    private Player _target;
    private bool _isActive;

    private void FixedUpdate()
    {
        if (_isActive)
        {
            Debug.Log(_target);
            Debug.Log("Несу игрока");
            _target.AddExternalForce(_windPower, _windDirection);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            Activate(player);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            Deactivate();
        }
    }

    private void Activate(Player player)
    {
        _target = player;
        _isActive = true;
    }

    private void Deactivate()
    {
        _isActive = false;
    }
}
