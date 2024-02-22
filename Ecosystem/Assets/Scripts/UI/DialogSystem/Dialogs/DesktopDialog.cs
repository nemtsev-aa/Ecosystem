using System;

public class DesktopDialog : Dialog {
    public event Action EcosystemDialogShowed;
    public event Action SettingsDialogShowed;
    public event Action AboutDialogShowed;
    public event Action Quited;

    private MenuCategoryPanel _category;

    public override void Init() {
        Logger.Instance.Log("Начало метода [DesktopDialog : Init]");
        base.Init();
    }

    public override void InitializationPanels() {
        _category = GetPanelByType<MenuCategoryPanel>();
        _category.Init();
    }

    public override void AddListeners() {
        base.AddListeners();

        _category.EcosystemDialogSelected += OnPolyhedrasDialogSelected;
        _category.SettingsDialogSelected += OnSettingsDialogSelected;
        _category.AboutDialogSelected += OnAboutDialogSelected;
        _category.QuitButtonSelected += OnQuitButtonSelected;
    }

    public override void RemoveListeners() {
        base.RemoveListeners();

        _category.EcosystemDialogSelected -= OnPolyhedrasDialogSelected;
        _category.SettingsDialogSelected -= OnSettingsDialogSelected;
        _category.AboutDialogSelected -= OnAboutDialogSelected;
        _category.QuitButtonSelected -= OnQuitButtonSelected;
    }


    private void OnPolyhedrasDialogSelected() => EcosystemDialogShowed?.Invoke();

    private void OnSettingsDialogSelected() => SettingsDialogShowed?.Invoke();

    private void OnAboutDialogSelected() => AboutDialogShowed?.Invoke();

    private void OnQuitButtonSelected() => Quited?.Invoke();

    public override void PreparingForClosure() { }
}
