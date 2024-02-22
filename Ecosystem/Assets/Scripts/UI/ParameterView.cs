using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ParameterView : Bar {
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _fillingValue;

    private LivingCreatureParameter _parameter;

    public void Init(LivingCreatureParameter parameter) {
        _parameter = parameter;

        AddListeners();
        OnValueChanged(_parameter.Value);
    }

    public override void AddListeners() {
        if (_parameter != null)
            _parameter.ValueChanged += OnValueChanged;
    }

    public override void RemoveListeners() {
        if (_parameter != null)
            _parameter.ValueChanged -= OnValueChanged;
    }

    public override void Reset() {
        Filler.fillAmount = _parameter.StartFilling;
    }

    protected override void OnValueChanged(float value) {
        if (Filler != null) {
            if (_parameter.MaxValue == 1) {
                Filler.fillAmount = value;
                _fillingValue.text = $"{Mathf.RoundToInt(value * 100)}%";
            }
            else 
            {
                Filler.fillAmount = value;
                _fillingValue.text = $"{Mathf.RoundToInt(Mathf.Clamp(value * 100, 0f, 100f))}%";
            }
        }
    }

    protected override void OnValueChanged(float currentValue, float maxValue) { }
}
