using System.Collections;
using TMPro;
using UnityEngine;

public class Panel : MonoBehaviour
{
    [SerializeField] private MySceneManager _sceneManager;
    [SerializeField] private TextMeshProUGUI _timeResult;
    [SerializeField] private TimeCounter _timeCounter;

    private float _timeToMove;
    private float _timeStepMove;
    private WaitForSeconds _moveOperation;
    private Coroutine _moveToScreenCoroutine;
    private float _targetXPosition;

    private void Awake()
    {
        _timeToMove = 1.2f;
        _timeStepMove = 0.05f;
        _moveOperation = new WaitForSeconds(_timeStepMove);
        _targetXPosition = 0;

        ShowResultTime();
    }

    public void MoveOnScreen()
    {
        _moveToScreenCoroutine = StartCoroutine(Move());
    }

    public void OnReloadLevelButtonClicked()
    {
        if (_moveToScreenCoroutine != null)
            StopCoroutine(_moveToScreenCoroutine);

        _sceneManager.ReloadLevel();
    }

    private IEnumerator Move()
    {
        int stepsCount = (int)(_timeToMove / _timeStepMove);
        int valuePerStep = (int)(Mathf.Abs(transform.position.x - _targetXPosition) / stepsCount);

        while (transform.position.x < _targetXPosition)
        {
            transform.position = GetNextPosition(valuePerStep);

            yield return _moveOperation;
        }

        transform.position = new Vector3(_targetXPosition, transform.position.y, transform.position.z);
    }

    private Vector3 GetNextPosition(int moveX)
    {
        Vector3 nextPosition = new Vector3(transform.position.x + moveX, transform.position.y, transform.position.z);

        return nextPosition;
    }

    private void ShowResultTime()
    {
        if (_timeCounter == null)
            return;

        _timeResult.text = _timeCounter.GetResultTime().ToString();
    }
}
