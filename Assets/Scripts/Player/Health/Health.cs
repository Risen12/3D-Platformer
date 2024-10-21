using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxHealth;

    private float _health;

    public event Action Damaged;
    public event Action<int> HealthChanged;
    public event Action Died;

    public float MaxHealth => _maxHealth;

    private void Awake()
    {
        _health = _maxHealth;
    }

    private void Start()
    {
        HealthChanged?.Invoke((int)_health);
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;

        Damaged?.Invoke();
        HealthChanged?.Invoke((int)_health);

        if (_health <= 0)
            Died?.Invoke();
    }
}
