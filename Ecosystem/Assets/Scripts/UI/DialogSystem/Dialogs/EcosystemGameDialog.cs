using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class EcosystemGameDialog : Dialog {
    public event Action CreatePlantClicked;
    public event Action CreatePlanktonClicked;
    public event Action RestartGameClicked;

    [SerializeField] private Button _createPlantButton;
    [SerializeField] private Button _createPlanktonButton;
    [SerializeField] private Button _restartGameButton;

    private ResultPanel _resultPanel;
    private EcosystemLifeTimePanel _ecosystemLifeTimePanel;
    private EcosystemParametersPanel _ecosystemParametersPanel;

    private EcosystemParametersConfig _config;
    private TimeCounter _timeCounter;

    [Inject]
    public void Construct(TimeCounter timeCounter, EcosystemParametersConfig config) {
        _timeCounter = timeCounter;
        _config = config;
    }

    public override void AddListeners() {
        base.AddListeners();

        _createPlantButton.onClick.AddListener(CreatePlantButtonClick);
        _createPlanktonButton.onClick.AddListener(CreatePlanktonButtonClick);
        _restartGameButton.onClick.AddListener(RestartGameButtonClick);
    }

   
    public override void RemoveListeners() {
        base.RemoveListeners();

        _createPlantButton.onClick.RemoveListener(CreatePlantButtonClick);
        _createPlanktonButton.onClick.RemoveListener(CreatePlanktonButtonClick);
        _restartGameButton.onClick.RemoveListener(RestartGameButtonClick);
    }

    public override void InitializationPanels() {
        _ecosystemParametersPanel = GetPanelByType<EcosystemParametersPanel>();
        _ecosystemParametersPanel.Init(_config);

        _ecosystemLifeTimePanel = GetPanelByType<EcosystemLifeTimePanel>();
        _ecosystemLifeTimePanel.Init(_timeCounter);
        _ecosystemLifeTimePanel.Show(true);

        _resultPanel = GetPanelByType<ResultPanel>();
        _resultPanel.Init(_timeCounter);
        _resultPanel.Show(false);
    }

    public override void PreparingForClosure() {
        
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

}
