using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuCategoryPanel : UIPanel {
    public event Action EcosystemDialogSelected;
    public event Action SettingsDialogSelected;
    public event Action AboutDialogSelected;
    public event Action QuitButtonSelected;

    [SerializeField] private Button _ecosystemButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _aboutButton;
    [SerializeField] private Button _quitButton;

    public void Init() {
        Logger.Instance.Log("Начало метода [MenuCategoryPanel : Init]");
        AddListeners();
    }

    public override void AddListeners() {
        base.AddListeners();

        _ecosystemButton.onClick.AddListener(EcosystemButtonClick);
        _settingsButton.onClick.AddListener(SettingsButtonClick);
        _aboutButton.onClick.AddListener(AboutButtonClick);
        _quitButton.onClick.AddListener(QuitButtonClick);
    }

    public override void RemoveListeners() {
        _ecosystemButton.onClick.RemoveListener(EcosystemButtonClick);
        _settingsButton.onClick.RemoveListener(SettingsButtonClick);
        _aboutButton.onClick.RemoveListener(AboutButtonClick);
        _quitButton.onClick.RemoveListener(QuitButtonClick);
    }

    public override void Show(bool value) {
        base.Show(value);

    }

    private void EcosystemButtonClick() => EcosystemDialogSelected?.Invoke();

    private void SettingsButtonClick() => SettingsDialogSelected?.Invoke();

    private void AboutButtonClick() => AboutDialogSelected?.Invoke();

    private void QuitButtonClick() => QuitButtonSelected?.Invoke();

}
