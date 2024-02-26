using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EcosystemParameterPanel : UIPanel {
    [SerializeField] private Image _filler;
    [SerializeField] private TextMeshProUGUI _valueText;
    [SerializeField] private Scrollbar _scrollBar;

    private EcosystemParameterConfig _config;
    
    private EcosystemParameterVariants _variant;
    public EcosystemParameterVariants CurrentValue => _variant;
    
    public void Init(EcosystemParameterConfig config) {
        _config = config;
        ShowEcosystemParametersValue();
        AddListeners();
    }

    public override void AddListeners() {
        base.AddListeners();

        _scrollBar.onValueChanged.AddListener(ParameterValueChanged);
    }

    public override void RemoveListeners() {
        base.RemoveListeners();

        _scrollBar.onValueChanged.RemoveListener(ParameterValueChanged);
    }

    private void ParameterValueChanged(float value) {
        _filler.fillAmount = value;

        SetVariant(value);

        _valueText.text = _config.GetText();
    }

    private void ShowEcosystemParametersValue() {
        var value = _config.GetValue();

        _filler.fillAmount = GetFillerValue();
        _scrollBar.value = GetFillerValue();

        _valueText.text = _config.GetText();
    }

    private void SetVariant(float value) {
        switch (value) {
            case 0f:
                _variant = EcosystemParameterVariants.ExtraLow;
                break;
            case 0.25f:
                _variant = EcosystemParameterVariants.Low;
                break;
            case 0.5f:
                _variant = EcosystemParameterVariants.Normal;
                break;
            case 0.75f:
                _variant = EcosystemParameterVariants.High;
                break;
            case 1f:
                _variant = EcosystemParameterVariants.ExtraHigh;
                break;
        }

        _config.Variant = _variant;
    }

    private float GetFillerValue() {
        switch (_config.Variant) {
            case EcosystemParameterVariants.ExtraLow:
                return 0.0f;

            case EcosystemParameterVariants.Low:
                return 0.25f;

            case EcosystemParameterVariants.Normal:
                return 0.5f;

            case EcosystemParameterVariants.High:
                return 0.75f;

            case EcosystemParameterVariants.ExtraHigh:
                return 1f;

            default:
                throw new ArgumentException($"Invalid EcosystemParameterVariant: {_variant}");
        }
    }

}
