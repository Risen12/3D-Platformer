using System.Collections;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class BasicTrap : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private LayerMask _playerLayerMask;

    private Coroutine _activeStateCoroutine;
    private WaitForSeconds _preActiveState;
    private WaitForSeconds _reloadState;
    private WaitForSeconds _activeState;
    private float _reloadTime;
    private float _preActivateTime;
    private float _activeTime;
    private bool _isActive;
    private Renderer _renderer;
    private Color _preActiveColor;
    private Color _activeColor;
    private Color _defaultColor;
    private Health _target;

    private void Awake()
    {
        _activeTime = 0.1f;
        _preActivateTime = 1f;
        _reloadTime = 5f;
        _preActiveState = new WaitForSeconds(_preActivateTime);
        _reloadState = new WaitForSeconds(_reloadTime);
        _activeState = new WaitForSeconds(_activeTime);
        _isActive = false;

        _renderer = GetComponent<Renderer>();

        _defaultColor = _renderer.material.color;
        _preActiveColor = Color.yellow;
        _activeColor = Color.red;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Health health))
        {
            _target = health;
            if (_isActive == false)
                Activate();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            if (_isActive)
                Deactivate();
        }
    }

    private void Activate()
    {
        _isActive = true;
        _activeStateCoroutine = StartCoroutine(ActiveState());
    }

    private void Deactivate()
    {
        _isActive = false;

        if (_activeStateCoroutine != null)
            StopCoroutine(_activeStateCoroutine);

        _renderer.material.color = _defaultColor;
    }

    private IEnumerator ActiveState()
    {
        while (_isActive)
        {
            _renderer.material.color = _preActiveColor;

            yield return _preActiveState;

            _renderer.material.color = _activeColor;

            _target.TakeDamage(_damage);

            yield return _activeState;

            _renderer.material.color = _defaultColor;

            yield return _reloadState;
        }
    }
}
