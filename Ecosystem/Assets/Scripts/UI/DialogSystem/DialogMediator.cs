using UnityEngine;
using System;
using System.Collections.Generic;

public class DialogMediator : IDisposable {
    private UIManager _uIManager;

    private DesktopDialog _desktopDialog;
    private EcosystemCreatorDialog _ecosystemCreatorDialog;
    private SettingsDialog _settingsDialog;
    private AboutDialog _aboutDialog;

    private DialogSwitcher _dialogSwitcher;
    private List<Dialog> _dialogs;

    public DialogMediator(UIManager uIManager, DialogSwitcher dialogSwitcher) {
        _uIManager = uIManager;
        _dialogSwitcher = dialogSwitcher;

    }

    public void Init() {
        GetDialogs();
        AddListeners();
    }

    private void GetDialogs() {
        _desktopDialog = _uIManager.GetDialogByType(DialogTypes.Desktop).GetComponent<DesktopDialog>();
        _settingsDialog = _uIManager.GetDialogByType(DialogTypes.Settings).GetComponent<SettingsDialog>();
        _aboutDialog = _uIManager.GetDialogByType(DialogTypes.About).GetComponent<AboutDialog>();
        _ecosystemCreatorDialog = _uIManager.GetDialogByType(DialogTypes.EcosystemCreator).GetComponent<EcosystemCreatorDialog>();

        _dialogs = new List<Dialog>() {
            _desktopDialog,
            _settingsDialog,
            _aboutDialog,
            _ecosystemCreatorDialog
        };
    }

    private void AddListeners() {
        foreach (var iDialog in _dialogs) {
            iDialog.SettingsClicked += OnSettingsClicked;
            iDialog.BackClicked += OnBackClicked;
        }

        SubscribeToDesktopDialogActions();
    }

    private void RemoveListeners() {
        foreach (var iDialog in _dialogs) {
            iDialog.BackClicked -= OnBackClicked;
            iDialog.SettingsClicked -= OnSettingsClicked;
        }

        UnSubscribeToDesktopDialogActions();
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
    }

    private void OnEcosystemDialogShowed() => _dialogSwitcher.ShowDialog(DialogTypes.EcosystemCreator);

    private void OnSettingsDialogShowed() => _dialogSwitcher.ShowDialog(DialogTypes.Settings);

    private void OnAboutDialogShowed() => _dialogSwitcher.ShowDialog(DialogTypes.About);

    private void OnQuited() => Application.Quit();

    #endregion

    public void Dispose() {
        RemoveListeners();
    }
}