using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BalanceTrap : MonoBehaviour
{
    private Coroutine _returnRotateCoroutine;
    private WaitForSeconds _returnRotate;
    private float _returnRotateTime;
    private float _timeToReturnRotate;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _returnRotateTime = 0.05f;
        _returnRotate = new WaitForSeconds(_returnRotateTime);
        _timeToReturnRotate = 4f;

        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_returnRotateCoroutine != null)
            StopCoroutine(_returnRotateCoroutine);
    }

    private void OnCollisionExit(Collision collision)
    {
        ReturnToStartRotation();
    }

    private void ReturnToStartRotation()
    {
        _returnRotateCoroutine = StartCoroutine(RotateCoroutine());
    }

    private IEnumerator RotateCoroutine()
    {
        _rigidbody.angularVelocity = Vector3.zero;
        float currentTime = 0f;
        float finishTime = 1f;

        while (currentTime < finishTime)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, currentTime);
            currentTime += Time.deltaTime / _timeToReturnRotate;

            yield return _returnRotate;
        }

        transform.rotation = Quaternion.identity;
    }
}