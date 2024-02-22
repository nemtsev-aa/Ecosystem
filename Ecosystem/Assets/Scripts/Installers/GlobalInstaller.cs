using UnityEngine;
using Zenject;

public class GlobalInstaller : MonoInstaller {
    [SerializeField] private TemperatureConfig _temperature;
    [SerializeField] private HumidityConfig _humidity;
    [SerializeField] private UICompanentPrefabs _uiCompanentPrefabs;

    public override void InstallBindings() {
        BindLogger();
        BindUICompanentsConfig();
        BindConfigs();
        BindFactories();
        BindTimeCounter();
    }

    private void BindLogger() {
        Logger logger = new Logger();

        Container.BindInstance(logger).AsSingle();
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

    private void BindTimeCounter() {
        TimeCounter timeCounter = new TimeCounter();

        Container.BindInstance(timeCounter).AsSingle();
        Container.BindInterfacesAndSelfTo<ITickable>().FromInstance(timeCounter).AsSingle();
    }
}