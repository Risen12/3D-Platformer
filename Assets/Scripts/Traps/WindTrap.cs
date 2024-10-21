using System.Collections;
using UnityEngine;

public class WindTrap : MonoBehaviour
{
    [SerializeField] private float _windPower;
    [SerializeField] private Vector3 _windDirection;

    private Player _target;
    private bool _isActive;
    private float _windChangeDelay;
    private WaitForSeconds _windChangeDirection;

    private void Awake()
    {
        _windChangeDelay = 2f;
        _windChangeDirection = new WaitForSeconds(_windChangeDelay);

        StartCoroutine(ChangeDirection());
    }

    private void FixedUpdate()
    {
        if (_isActive)
        {
            _target.AddExternalForce(_windDirection * _windPower);
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

    private IEnumerator ChangeDirection()
    {
        float minMultiplyer = -1;
        float maxMultiplyer = 1;

        while (gameObject.activeInHierarchy)
        {
            yield return _windChangeDirection;

            float multiplyer = Random.Range(minMultiplyer, maxMultiplyer);

            if (multiplyer < 0)
            {
                _windDirection *= minMultiplyer;
            }
            else
            {
                _windDirection *= maxMultiplyer;
            }
        }
    }
}
