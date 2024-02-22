using UnityEngine;
using Zenject;

public class GlobalInstaller : MonoInstaller {
    [SerializeField] private TemperatureConfig _temperature;
    [SerializeField] private HumidityConfig _humidity;
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

        EcosystemManager ecosystemManager = new EcosystemManager(_temperature, _humidity, timeCounter);
        Container.BindInstance(ecosystemManager).AsSingle();

        DialogMediator dialogMediator = new DialogMediator(_temperature, _humidity, ecosystemManager);
        Container.BindInstance(dialogMediator).AsSingle();
    }

    private void BindConfigs() {
        if (_temperature == null)
            Debug.LogError($"TemperatureConfig is empty");

        Container.Bind<TemperatureConfig>().FromInstance(_temperature).AsSingle();

        if (_humidity == null)
            Debug.LogError($"HumidityConfig is empty");

        Container.Bind<HumidityConfig>().FromInstance(_humidity).AsSingle();
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