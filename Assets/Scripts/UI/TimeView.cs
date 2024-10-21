using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TimeView : MonoBehaviour
{
    [SerializeField] private TimeCounter _timeCounter;

    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        _timeCounter.TimeChanged += OnTimeChanged;
    }

    private void OnDisable()
    {
        _timeCounter.TimeChanged += OnTimeChanged;
    }

    private void OnTimeChanged(TimeSpan currentTime)
    {
        _text.text = currentTime.ToString("mm\\:ss");
    }
}
