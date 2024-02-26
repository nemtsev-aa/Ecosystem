using System;
using UnityEngine;

public class EcosystemManager {
    public event Action SimulationStarted;
    public event Action<float> SimulationStopped;

    private TemperatureConfig _temperature;
    private HumidityConfig _humidity;
    private TimeCounter _timeCounter;

    public EcosystemManager(EcosystemParametersConfig ecosystemParametersConfig, TimeCounter timeCounter) {
        _temperature = ecosystemParametersConfig.TemperatureConfig;
        _humidity = ecosystemParametersConfig.HumidityConfig;
        _timeCounter = timeCounter;
    }

    [field: SerializeField] public float ExistenceTime { get; private set; }
    [field: SerializeField] public bool IsStarted { get; private set; }

    public void StartSimulation() {
        IsStarted = true;
        _timeCounter.SetWatchStatus(true);

        SimulationStarted?.Invoke();
    }

    public void StopSimulation() {
        IsStarted = false;
        _timeCounter.SetWatchStatus(false);

        SimulationStopped?.Invoke(ExistenceTime); 
    }
}
