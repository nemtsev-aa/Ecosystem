using UnityEngine;

public class GrowingState : IState {
    public IStateSwitcher StateSwitcher { get; set; }

    private LivingCreatureStateMachineData _data;
    private LivingCreature _livingCreature;

    public GrowingState(LivingCreatureStateMachineData data, LivingCreature livingCreature) {
        _data = data;
        _livingCreature = livingCreature;
    }

    public void Enter() {
        Debug.Log($"DrowingState: Enter");

    }

    public void Exit() {
        Debug.Log($"DrowingState: Exit");
    }

    public void Update() {
        if (_livingCreature is Animal) {
            _livingCreature.transform.localScale += Vector3.one * 0.01f;
            StateSwitcher.SwitchState<WalkingState>();
        }
            
        if (_livingCreature is Plant) {
            _livingCreature.transform.localScale += Vector3.one * 0.02f;
            StateSwitcher.SwitchState<PlantIdlingState>();
        }   
    }
}
