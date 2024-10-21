using UnityEngine;

public class CircleTrap : MonoBehaviour
{
    [SerializeField] private float _rotatePower;

    private Vector3 _rotateDirection;

    private void Awake()
    {
        _rotateDirection = new Vector3(0, _rotatePower, 0);
    }

    private void Update()
    {
        transform.Rotate(_rotateDirection * Time.deltaTime);
    }
}
