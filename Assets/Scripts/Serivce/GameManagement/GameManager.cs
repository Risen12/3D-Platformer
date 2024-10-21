using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private Panel _winPanel;
    [SerializeField] private Panel _losePanel;
    [SerializeField] private Health _health;
    [SerializeField] private Line _finishLine;
    [SerializeField] private CanvasGroup _hudCanvas;
    [SerializeField] private Camera _mainCamera;

    private float _timeStep;
    private WaitForSeconds _hideHUDOperation;
    private Vector3 _cameraDefaultPosition;
    private Quaternion _cameraDefaultRotation;

    private void Awake()
    {
        _timeStep = 0.05f;
        _hideHUDOperation = new WaitForSeconds(_timeStep);
        _cameraDefaultPosition = new Vector3(820f, 417f, 443f);
        _cameraDefaultRotation = Quaternion.Euler(16f,-126f,0f);
    }

    private void OnEnable()
    {
        _health.Died += OnPlayerDied;
        _finishLine.PlayerCrossed += OnPlayerWon;
    }

    private void OnDisable()
    {
        _health.Died -= OnPlayerDied;
        _finishLine.PlayerCrossed -= OnPlayerWon;
    }

    private void OnPlayerDied()
    {
        PrepareResultScreen(_losePanel);
    }

    private void OnPlayerWon()
    {
        PrepareResultScreen(_winPanel);
    }

    private void PrepareResultScreen(Panel panel)
    {
        panel.gameObject.SetActive(true);
        PrepareCamera();
        panel.MoveOnScreen();
        _inputReader.enabled = false;
        StartCoroutine(HideHUD());
    }

    private IEnumerator HideHUD()
    {
        while (_hudCanvas.alpha > 0)
        {
            _hudCanvas.alpha -= _timeStep;
            yield return _hideHUDOperation;
        }

        _hudCanvas.alpha = 0;
    }

    private void PrepareCamera()
    {
        float deltaStep = 1000f;

        _mainCamera.transform.position = Vector3.MoveTowards(_mainCamera.transform.position, _cameraDefaultPosition, deltaStep);
        _mainCamera.transform.rotation = _cameraDefaultRotation;
        _mainCamera.transform.parent = null;
    }
}
