using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TextMeshProUGUI))]
public class HealthView : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private Slider _slider;

    private TextMeshProUGUI _text;
    private float _timeToChangeValue;
    private float _timeStepChangeValue;
    private WaitForSeconds _changeValue;
    private Coroutine _changeHealthBarValueCoroutine;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _timeToChangeValue = 1f;
        _timeStepChangeValue = 0.1f;
        _changeValue = new WaitForSeconds(_timeStepChangeValue);
    }

    private void OnEnable()
    {
        _health.HealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        _health.HealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(int currentHealth)
    { 
        if (_changeHealthBarValueCoroutine != null)
            StopCoroutine(_changeHealthBarValueCoroutine);

        _changeHealthBarValueCoroutine = StartCoroutine(ChangeHealthBarValue(currentHealth));
    }

    private IEnumerator ChangeHealthBarValue(int currentHealth)
    {
        float healthValue = _health.MaxHealth - currentHealth;
        int healthValueStep = (int)(healthValue / (_timeToChangeValue / _timeStepChangeValue));

        while (_slider.value > currentHealth)
        {
            _slider.value -= healthValueStep;
            _text.text = _slider.value.ToString();
            yield return _changeValue;
        }
    }
}
