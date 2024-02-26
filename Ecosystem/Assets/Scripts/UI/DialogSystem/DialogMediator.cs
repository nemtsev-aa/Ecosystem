using UnityEngine;
using System;
using System.Collections.Generic;

public class DialogMediator : IDisposable {
    public const float MaxLivingCreatureCount = 30f;

    private UIManager _uIManager;

    private DesktopDialog _desktopDialog;
    private EcosystemCreatorDialog _ecosystemCreatorDialog;
    private EcosystemGameDialog _ecosystemGameDialog;
    private SettingsDialog _settingsDialog;
    private AboutDialog _aboutDialog;

    private DialogSwitcher _dialogSwitcher;
    private List<Dialog> _dialogs;


    private EcosystemManager _ecosystemManager;
    private LivingCreatureSpawner _spawner => _uIManager.Spawner;

    public DialogMediator(EcosystemManager ecosystemManager) {
        _ecosystemManager = ecosystemManager;
    }

    public void Init(UIManager uIManager, DialogSwitcher dialogSwitcher) {
        _uIManager = uIManager;
        _dialogSwitcher = dialogSwitcher;
        
        GetDialogs();
        AddListeners();
    }

    private void GetDialogs() {
        _desktopDialog = _uIManager.GetDialogByType(DialogTypes.Desktop).GetComponent<DesktopDialog>();
        _settingsDialog = _uIManager.GetDialogByType(DialogTypes.Settings).GetComponent<SettingsDialog>();
        _aboutDialog = _uIManager.GetDialogByType(DialogTypes.About).GetComponent<AboutDialog>();
        _ecosystemCreatorDialog = _uIManager.GetDialogByType(DialogTypes.EcosystemCreator).GetComponent<EcosystemCreatorDialog>();
        _ecosystemGameDialog = _uIManager.GetDialogByType(DialogTypes.EcosystemGame).GetComponent<EcosystemGameDialog>();

        _dialogs = new List<Dialog>() {
            _desktopDialog,
            _settingsDialog,
            _aboutDialog,
            _ecosystemCreatorDialog,
            _ecosystemGameDialog
        };
    }

    private void AddListeners() {
        foreach (var iDialog in _dialogs) {
            iDialog.SettingsClicked += OnSettingsClicked;
            iDialog.BackClicked += OnBackClicked;
        }

        SubscribeToDesktopDialogActions();
        SubscribeToEcosystemCreatorDialogActions();
        SubscribeToEcosystemGameDialogActions();
    }

    private void RemoveListeners() {
        foreach (var iDialog in _dialogs) {
            iDialog.BackClicked -= OnBackClicked;
            iDialog.SettingsClicked -= OnSettingsClicked;
        }

        UnSubscribeToDesktopDialogActions();
        UnSubscribeToEcosystemCreatorDialogActions();
        UnSubscribeToEcosystemGameDialogActions();
    }

    private void OnBackClicked() => _dialogSwitcher.ShowPreviousDialog();

    private void OnSettingsClicked() => _dialogSwitcher.ShowDialog(DialogTypes.Settings);

    #region DesktopDialogActions
    private void SubscribeToDesktopDialogActions() {
        _desktopDialog.EcosystemDialogShowed += OnEcosystemDialogShowed;
        _desktopDialog.SettingsDialogShowed += OnSettingsDialogShowed;
        _desktopDialog.AboutDialogShowed += OnAboutDialogShowed;
        _desktopDialog.Quited += OnQuited;
        _desktopDialog.BackClicked += OnQuited;
    }

    private void UnSubscribeToDesktopDialogActions() {
        _desktopDialog.EcosystemDialogShowed -= OnEcosystemDialogShowed;
        _desktopDialog.SettingsDialogShowed -= OnSettingsDialogShowed;
        _desktopDialog.AboutDialogShowed -= OnAboutDialogShowed;
        _desktopDialog.Quited -= OnQuited;
        _desktopDialog.BackClicked -= OnQuited;
    }

    private void OnEcosystemDialogShowed() => _dialogSwitcher.ShowDialog(DialogTypes.EcosystemCreator);

    private void OnSettingsDialogShowed() => _dialogSwitcher.ShowDialog(DialogTypes.Settings);

    private void OnAboutDialogShowed() => _dialogSwitcher.ShowDialog(DialogTypes.About);

    private void OnQuited() => Application.Quit();

    #endregion

    #region EcosystemCreatorDialogActions
    private void SubscribeToEcosystemCreatorDialogActions() {
        _ecosystemCreatorDialog.ParameterVariantSelected += OnParameterVariantSelected;
    }

    private void UnSubscribeToEcosystemCreatorDialogActions() {
        _ecosystemCreatorDialog.ParameterVariantSelected -= OnParameterVariantSelected;
    }

    private void OnParameterVariantSelected(EcosystemParameterVariants temperature, EcosystemParameterVariants humidity) {
        _dialogSwitcher.ShowDialog(DialogTypes.EcosystemGame);
    }
    #endregion

    #region EcosystemGameDialogActions
    private void SubscribeToEcosystemGameDialogActions() {
        _ecosystemGameDialog.CreatePlantClicked += OnCreatePlantClicked;
        _ecosystemGameDialog.CreatePlanktonClicked += OnCreatePlanktonClicked;
        _ecosystemGameDialog.RestartGameClicked += OnRestartGameClicked;
        _ecosystemGameDialog.ViewsVisibleChanged += OnViewsVisibleChanged;

        _spawner.AnimalCountChanged += OnAnimalCountChanged;
        _spawner.PlantCountChanged += OnPlantCountChanged;
    }

    private void OnCreatePlantClicked() {
        if (_ecosystemManager.IsStarted == false)
            _ecosystemManager.StartSimulation();

        _spawner.CreatePlant();
    }

    private void OnCreatePlanktonClicked() {
        if (_ecosystemManager.IsStarted == false)
            _ecosystemManager.StartSimulation();

        _spawner.CreateAnimal();
    }

    private void OnRestartGameClicked() {
        _spawner.Reset();
        _ecosystemGameDialog.ShowPanel<ResultPanel>(false);

        OnBackClicked();
    }
    
    private void OnPlantCountChanged(int plantsCount) {
        if (plantsCount == 0 || plantsCount > MaxLivingCreatureCount) {
            _ecosystemGameDialog.ShowPanel<ResultPanel>(true);
            _ecosystemManager.StopSimulation();
        }  
    }

    private void OnAnimalCountChanged(int animalCount) {
        if (animalCount == 0 || animalCount > MaxLivingCreatureCount) {
            _ecosystemGameDialog.ShowPanel<ResultPanel>(true);
            _ecosystemManager.StopSimulation();
        }    
    }
    
    private void OnViewsVisibleChanged(bool status) => _spawner.ShowViews(status);

    private void UnSubscribeToEcosystemGameDialogActions() {
        _ecosystemGameDialog.CreatePlantClicked -= OnCreatePlantClicked;
        _ecosystemGameDialog.CreatePlanktonClicked -= OnCreatePlanktonClicked;
        _ecosystemGameDialog.RestartGameClicked -= OnRestartGameClicked;
        _ecosystemGameDialog.ViewsVisibleChanged -= OnViewsVisibleChanged;

        _spawner.AnimalCountChanged -= OnAnimalCountChanged;
        _spawner.PlantCountChanged -= OnPlantCountChanged;
    }
    
    #endregion

   
    public void Dispose() {
        RemoveListeners();
    }
}