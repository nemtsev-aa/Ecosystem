using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class UIManager : MonoBehaviour, IDisposable {
    [SerializeField] private RectTransform _dialogsParent;

    private UICompanentsFactory _companentsFactory;
    private DialogFactory _dialogFactory;
    private Logger _logger;

    private DialogSwitcher _dialogSwitcher;
    private DialogMediator _dialogMediator;

    private Dictionary<DialogTypes, Dialog> _dialogsDictionary;
    private List<Dialog> _dialogs;

    [Inject]
    public void Construct(UICompanentsFactory companentsFactory, DialogFactory dialogFactory, Logger logger) {
        _companentsFactory = companentsFactory;
        _logger = logger;

        _dialogFactory = dialogFactory;
        _dialogFactory.SetDialogsParent(_dialogsParent);

        CreateDialogs();

        _dialogSwitcher = new DialogSwitcher(this);
        _dialogMediator = new DialogMediator(this, _dialogSwitcher);
        _dialogMediator.Init();

        //_dialogMediator.PolyhedraSelected += OnPolyhedraSelected;
        //_dialogMediator.PolyhedraCompanentSelected += OnPolyhedraCompanentSelected;
        //_dialogMediator.JoysticValueChanged += OnJoysticValueChanged;
        //_dialogMediator.ColorSettingsChanged += OnColorSettingsChanged;
    }

    public void Init() {
        _dialogSwitcher.ShowDialog(DialogTypes.Desktop);
    }

    public Dialog GetDialogByType(DialogTypes type) {
        if (_dialogsDictionary.Keys.Count == 0)
            throw new ArgumentNullException("DialogsDictionary is empty");

        return _dialogsDictionary[type];
    }

    public List<Dialog> GetDialogList() {
        return _dialogsDictionary.Values.ToList();
    }

    private void CreateDialogs() {
        _dialogsDictionary = new Dictionary<DialogTypes, Dialog> {
                { DialogTypes.Desktop, _dialogFactory.GetDialog<DesktopDialog>()},
                { DialogTypes.Settings, _dialogFactory.GetDialog<SettingsDialog>()},
                { DialogTypes.About, _dialogFactory.GetDialog<AboutDialog>()},
                { DialogTypes.EcosystemCreator, _dialogFactory.GetDialog<EcosystemCreatorDialog>()},
            };

        foreach (var iDialog in _dialogsDictionary.Values) {
            iDialog.Init(_logger);
            iDialog.Show(false);
        }
    }

    //private void OnPolyhedraSelected(PolyhedraConfig config) => ModelsRotated?.Invoke(config);

    //private void OnPolyhedraCompanentSelected(PolyhedrasCompanentTypes type) => CompanentBlinked?.Invoke(type);

    //private void OnJoysticValueChanged(float horizontal, float vertical) => InputValueChanged?.Invoke(horizontal, vertical);

    //private void OnColorSettingsChanged() => ColorSettingsChanged?.Invoke();

    public void Dispose() {
        //_dialogMediator.PolyhedraSelected -= OnPolyhedraSelected;
        //_dialogMediator.PolyhedraCompanentSelected -= OnPolyhedraCompanentSelected;
        //_dialogMediator.JoysticValueChanged -= OnJoysticValueChanged;
        //_dialogMediator.ColorSettingsChanged -= OnColorSettingsChanged;
    }
}
