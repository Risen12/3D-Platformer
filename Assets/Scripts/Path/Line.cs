using System;
using UnityEngine;

public class Line : MonoBehaviour
{
    public event Action PlayerCrossed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            PlayerCrossed?.Invoke();
        }
    }
}
