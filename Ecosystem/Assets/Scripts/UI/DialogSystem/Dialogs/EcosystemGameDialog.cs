using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class EcosystemGameDialog : Dialog {
    public event Action CreatePlantClicked;
    public event Action CreatePlanktonClicked;
    public event Action RestartGameClicked;
    public event Action<bool> ViewsVisibleChanged;

    [SerializeField] private Button _createPlantButton;
    [SerializeField] private Button _createPlanktonButton;
    [SerializeField] private Button _restartGameButton;
    [SerializeField] private Button _viewsButton;

    private ResultPanel _resultPanel;
    private EcosystemLifeTimePanel _ecosystemLifeTimePanel;
    private EcosystemParametersPanel _ecosystemParametersPanel;

    private EcosystemParametersConfig _config;
    private TimeCounter _timeCounter;
    private bool IsShowViews = false;

    [Inject]
    public void Construct(TimeCounter timeCounter, EcosystemParametersConfig config) {
        _timeCounter = timeCounter;
        _config = config;
    }

    public override void Show(bool value) {
        base.Show(value);

        if (value == true) {
            _ecosystemParametersPanel.Init(_config);

            SetBackgroundColor();
        }
    }

    public override void AddListeners() {
        base.AddListeners();

        _createPlantButton.onClick.AddListener(CreatePlantButtonClick);
        _createPlanktonButton.onClick.AddListener(CreatePlanktonButtonClick);
        _restartGameButton.onClick.AddListener(RestartGameButtonClick);
        _viewsButton.onClick.AddListener(ViewsButtonClick);
    }

    public override void RemoveListeners() {
        base.RemoveListeners();

        _createPlantButton.onClick.RemoveListener(CreatePlantButtonClick);
        _createPlanktonButton.onClick.RemoveListener(CreatePlanktonButtonClick);
        _restartGameButton.onClick.RemoveListener(RestartGameButtonClick);
        _viewsButton.onClick.RemoveListener(ViewsButtonClick);
    }

    public override void InitializationPanels() {
        _ecosystemParametersPanel = GetPanelByType<EcosystemParametersPanel>();

        _ecosystemLifeTimePanel = GetPanelByType<EcosystemLifeTimePanel>();
        _ecosystemLifeTimePanel.Init(_timeCounter);
        _ecosystemLifeTimePanel.Show(true);

        _resultPanel = GetPanelByType<ResultPanel>();
        _resultPanel.Init(_timeCounter);
        _resultPanel.Show(false);
    }

    public override void PreparingForClosure() {
        RestartGameClicked?.Invoke();
    }

    private void CreatePlantButtonClick() {
        CreatePlantClicked?.Invoke();
    }

    private void CreatePlanktonButtonClick() {
        CreatePlanktonClicked?.Invoke();
    }

    private void RestartGameButtonClick() {
        RestartGameClicked?.Invoke();
    }

    private void ViewsButtonClick() {
        IsShowViews = !IsShowViews;

        ViewsVisibleChanged?.Invoke(IsShowViews);
    }

    private void SetBackgroundColor() {
        Color defaultColor = Camera.main.backgroundColor;
        float temperatureFactor = GetGreen—hannelValueByTemperature();

        var newColor = new Color(defaultColor.r, defaultColor.g, temperatureFactor, defaultColor.a);
        Camera.main.backgroundColor = newColor;
    }

    private float GetGreen—hannelValueByTemperature() {
        switch (_config.TemperatureConfig.Variant) {
            case EcosystemParameterVariants.ExtraLow:
                return 1f;

            case EcosystemParameterVariants.Low:
                return 0.75f;

            case EcosystemParameterVariants.Normal:
                return 0.5f;

            case EcosystemParameterVariants.High:
                return 0.25f;

            case EcosystemParameterVariants.ExtraHigh:
                return 0f;

            default:
                throw new ArgumentException($"Invalid EcosystemParameterVariant: {_config.TemperatureConfig.Variant}");
        }
    }
}
