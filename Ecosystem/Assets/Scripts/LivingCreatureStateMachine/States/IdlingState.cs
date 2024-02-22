using UnityEngine;

public class IdlingState : IState {
    private Animal _animal;
    private LivingCreatureStateMachineData _data;
    private AnimalParameters _parameters;

    private float _energy => _parameters.Energy.Value;
    private float _hunger => _parameters.Hunger.Value;
    private float _damage => _parameters.Damage / 100f;

    public IdlingState(LivingCreatureStateMachineData data, Animal animal) {
        _animal = animal;
        _data = data;
        _parameters = _animal.Parameters;
    }

    public IStateSwitcher StateSwitcher { get; set; }


    public void Enter() {
        Debug.Log($"IdlingState: Enter");
    }

    public void Exit() {
        Debug.Log($"IdlingState: Exit");
    }

    public void Update() {
        _parameters.AddEnergy();
        _parameters.TakeDamage(_damage);

        StateSwitcher.SwitchState<WalkingState>();
    }
}
