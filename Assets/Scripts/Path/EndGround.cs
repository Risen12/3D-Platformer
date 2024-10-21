using UnityEngine;

public class EndGround : MonoBehaviour
{
    private const float _damage = 100f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Health health))
        {
            health.TakeDamage(_damage);
        }
    }
}
