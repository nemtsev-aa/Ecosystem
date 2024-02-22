using System;
using UnityEngine;
using UnityEngine.UI;

public class EcosystemCreatorDialog : Dialog {
    public event Action<EcosystemParameterVariants, EcosystemParameterVariants> ParameterVariantSelected;

    [SerializeField] private Button _startGameButton;
    [SerializeField] private Image _temperatureFiller;
    [SerializeField] private Image _humidityFiller;

    private EcosystemParameterVariants _temperature = EcosystemParameterVariants.Normal;
    private EcosystemParameterVariants _humidity = EcosystemParameterVariants.Normal;
    
    public override void AddListeners() {
        base.AddListeners();
        _startGameButton.onClick.AddListener(OnStartGameButtonClick);
    }

    public override void RemoveListeners() {
        base.RemoveListeners();
        _startGameButton.onClick.RemoveListener(OnStartGameButtonClick);
    }

    private void OnStartGameButtonClick() {
        GetTemperatureVariant();
        GetHumidityVariant();

        ParameterVariantSelected?.Invoke(_temperature, _humidity);
    }

    private void GetHumidityVariant() {
        switch (_humidityFiller.fillAmount) {
            case 0f:
                _humidity = EcosystemParameterVariants.ExtraLow;
                break;
            case 0.25f:
                _humidity = EcosystemParameterVariants.Low;
                break;
            case 0.5f:
                _humidity = EcosystemParameterVariants.Normal;
                break;
            case 0.75f:
                _humidity = EcosystemParameterVariants.High;
                break;
            case 1f:
                _humidity = EcosystemParameterVariants.ExtraHigh;
                break;
        }
    }

    private void GetTemperatureVariant() {
        switch (_temperatureFiller.fillAmount) {
            case 0f:
                _temperature = EcosystemParameterVariants.ExtraLow;
                break;
            case 0.25f:
                _temperature = EcosystemParameterVariants.Low;
                break;
            case 0.5f:
                _temperature = EcosystemParameterVariants.Normal;
                break;
            case 0.75f:
                _temperature = EcosystemParameterVariants.High;
                break;
            case 1f:
                _temperature = EcosystemParameterVariants.ExtraHigh;
                break;
        }
    }

    public override void InitializationPanels() { }

    public override void PreparingForClosure() { }
}
