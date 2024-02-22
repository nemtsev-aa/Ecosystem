using System;
using UnityEngine;
using Zenject;

public class EcosystemManager : MonoBehaviour {
    public event Action SimulationStarted;
    public event Action<float> SimulationStopped;

    private TemperatureConfig _temperature;
    private HumidityConfig _humidity;
    private TimeCounter _timeCounter;

    [field: SerializeField] public float ExistenceTime { get; private set; }
    [field: SerializeField] public bool IsStarted { get; private set; }

    [Inject]
    public void Construct(TemperatureConfig temperature, HumidityConfig humidity, TimeCounter timeCounter) {
        _temperature = temperature;
        _humidity = humidity;
        _timeCounter = timeCounter;
    }

    public void StartSimulation() {
        IsStarted = true;
        SimulationStarted?.Invoke();
    }

    public void StopSimulation() {
        IsStarted = false;
        SimulationStopped?.Invoke(ExistenceTime);
    }

    private void Update() {
        if (IsStarted == false)
            return;
    }
}
