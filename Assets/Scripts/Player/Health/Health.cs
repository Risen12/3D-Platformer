using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxHealth;

    private float _health;

    private void Awake()
    {
        _health = _maxHealth;
    }

    public void TakeDamage(float damage)
    { 
        _health -= damage;

        if (_health <= 0)
            Die();
    }

    private void Die()
    { 
        
    }
}
