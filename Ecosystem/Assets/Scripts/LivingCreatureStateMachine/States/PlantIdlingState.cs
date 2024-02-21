using UnityEngine;

public class PlantIdlingState : IState {
    private Plant _plant;
    private PlantParameters _parameters;

    public PlantIdlingState(LivingCreatureStateMachineData data, Plant plant) {
        _plant = plant;
        _parameters = _plant.Parameters;
    }

    public IStateSwitcher StateSwitcher { get; set; }

    public void Enter() {
        Debug.Log($"PlantIdlingState: Enter");
    }

    public void Exit() {
        Debug.Log($"PlantIdlingState: Exit");
    }

    public void Update() {
        _parameters.AddGrowing();
        _parameters.AddDesireReproduction();

        if (_parameters.Grow.Value == 0f)
            StateSwitcher.SwitchState<GrowingState>();

        if (_parameters.ReproductionDesire.Value >= 1f)
            StateSwitcher.SwitchState<ReproductionState>();
    }
}
