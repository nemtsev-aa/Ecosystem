using UnityEngine;
using Zenject;

public class GlobalInstaller : MonoInstaller {
    [SerializeField] private EcosystemParametersConfig _ecosystemParametersConfig;
    [SerializeField] private UICompanentPrefabs _uiCompanentPrefabs;

    public override void InstallBindings() {
        BindServices();
        BindUICompanentsConfig();
        BindConfigs();
        BindFactories();
    }

    private void BindServices() {
        Logger logger = new Logger();
        Container.BindInstance(logger).AsSingle();

        TimeCounter timeCounter = new TimeCounter();
        Container.BindInstance(timeCounter).AsSingle();
        Container.BindInterfacesAndSelfTo<ITickable>().FromInstance(timeCounter).AsSingle();

        EcosystemManager ecosystemManager = new EcosystemManager(_ecosystemParametersConfig, timeCounter);
        Container.BindInstance(ecosystemManager).AsSingle();

        DialogMediator dialogMediator = new DialogMediator(_ecosystemParametersConfig, ecosystemManager);
        Container.BindInstance(dialogMediator).AsSingle();
    }

    private void BindConfigs() {
        if (_ecosystemParametersConfig == null)
            Debug.LogError($"EcosystemParametersConfig is empty");

        Container.Bind<EcosystemParametersConfig>().FromInstance(_ecosystemParametersConfig).AsSingle();
    }

    private void BindUICompanentsConfig() {
        if (_uiCompanentPrefabs.Prefabs.Count == 0)
            Debug.LogError($"List of UICompanentPrefabs is empty");

        Container.Bind<UICompanentPrefabs>().FromInstance(_uiCompanentPrefabs).AsSingle();
    }

    private void BindFactories() {
        Container.Bind<DialogFactory>().AsSingle();
        Container.Bind<UICompanentsFactory>().AsSingle();
    }
}