using System;
using UnityEngine;

public class EcosystemManager {
    public event Action SimulationStarted;
    public event Action<float> SimulationStopped;

    private TimeCounter _timeCounter;

    public EcosystemManager(TimeCounter timeCounter) {
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
        _timeCounter.Reset();

        SimulationStopped?.Invoke(ExistenceTime); 
    }
}
