using UnityEngine;

public class CircleTrapPart : MonoBehaviour
{
    [SerializeField] private float _force;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            Vector3 direction = player.transform.position - collision.GetContact(0).point;
            direction = new Vector3(direction.x, 0, direction.z);

            Debug.DrawRay(transform.position, direction * _force, Color.red, 15);
            player.AddExternalForce(direction * _force);
        }
    }
}
