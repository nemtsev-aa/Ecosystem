using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class EcosystemCreatorDialog : Dialog {
    public event Action<EcosystemParameterVariants, EcosystemParameterVariants> ParameterVariantSelected;

    [SerializeField] private Button _startGameButton;
    private TemperaturePanel _temperaturePanel;
    private HumidityPanel _humidityPanel;

    private EcosystemParametersConfig _config;

    [Inject]
    public void Construct(EcosystemParametersConfig config) {
        _config = config;
    }

    public override void InitializationPanels() {
        _temperaturePanel = GetPanelByType<TemperaturePanel>();
        _temperaturePanel.Init(_config.TemperatureConfig);

        _humidityPanel = GetPanelByType<HumidityPanel>();
        _humidityPanel.Init(_config.HumidityConfig);
    }

    public override void AddListeners() {
        base.AddListeners();
        _startGameButton.onClick.AddListener(OnStartGameButtonClick);
    }

    public override void RemoveListeners() {
        base.RemoveListeners();
        _startGameButton.onClick.RemoveListener(OnStartGameButtonClick);
    }

    private void OnStartGameButtonClick() {
        ParameterVariantSelected?.Invoke(_temperaturePanel.CurrentValue, _humidityPanel.CurrentValue);
    }

    public override void PreparingForClosure() { }
}
