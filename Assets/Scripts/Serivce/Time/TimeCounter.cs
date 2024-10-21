using System;
using UnityEngine;

public class TimeCounter : MonoBehaviour
{
    [SerializeField] private Line _startLine;
    [SerializeField] private Line _finishLine;
    [SerializeField] private Health _playerHealth;

    private float _time;
    private bool _isActive;

    public event Action<TimeSpan> TimeChanged;

    private void OnEnable()
    {
        _startLine.PlayerCrossed += StartTime;
        _finishLine.PlayerCrossed += EndTime;
        _playerHealth.Died += EndTime;
    }

    private void OnDisable()
    {
        _startLine.PlayerCrossed -= StartTime;
        _finishLine.PlayerCrossed -= EndTime;
        _playerHealth.Died -= EndTime;
    }

    private void Update()
    {
        if (_isActive)
        {
            _time += Time.deltaTime;
            VerifyTime();
        }
    }

    public TimeSpan GetResultTime()
    {
        int minutes = (int)_time;
        int seconds = (int)((_time / 10) * 100);

        return new TimeSpan(0, minutes, seconds);
    }

    private void StartTime()
    {
        if (_isActive == false)
        {
            _time = 0;
            _isActive = true;
        }
    }

    private void EndTime()
    {
        _isActive = false;
    }

    private void VerifyTime()
    {
        float minutesSplitter = 60f;

        int minutes = (int)(_time / minutesSplitter);
        int seconds = (int)(_time % minutesSplitter);
        TimeChanged?.Invoke(new TimeSpan(0, minutes, seconds));
    }
}
