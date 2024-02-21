using UnityEngine;

public class EatingState : IState {
    public IStateSwitcher StateSwitcher { get; set; }

    protected readonly LivingCreatureStateMachineData Data;
    protected readonly Animal Animal;
    private AnimalParameters _parameters;

    private LivingCreature _food;
    private float _eatingDuration => _parameters.EatingDuration;
    private float _energy => _parameters.Energy.Value;
    private float _time;

    public EatingState(LivingCreatureStateMachineData data, Animal animal) {
        Data = data;
        Animal = animal;
        _parameters = animal.Parameters;
    }

    public void Enter() {
        Debug.Log($"EatingState: Enter");
        _time = 0f;
        _food = Data.FoodTarget;
    }

    public void Exit() {
        Debug.Log($"EatingState: Exit");
        Data.FoodTarget = _food;
        Data.TargetPosition = Vector2.zero;
    }

    public void Update() {
        
        if (_food == null) {
            StateSwitcher.SwitchState<WalkingState>();
            return;
        }

        _time += Time.deltaTime;
        if (_time <= _eatingDuration)
            return;

        if (_food is Animal)
            TakeDamageToAnimal();

        if (_food is Plant)
            TakeDamageToPlant();

        if (_energy > 0.8f)
            StateSwitcher.SwitchState<GrowingState>();
    }

    private void TakeDamageToPlant() {
        var food = (Plant)_food;

        if (food.Parameters.Health.Value == 0) {
            _food = null;
            return;
        }

        food.Parameters.TakeDamage(_parameters.Damage);
        _parameters.TakeHunger();
        _parameters.AddEnergy();
    }

    private void TakeDamageToAnimal() {
        var food = (Animal)_food;

        if (food.Parameters.Health.Value == 0) {
            _food = null;
            return;
        }

        food.Parameters.TakeDamage(_parameters.Damage);
        _parameters.TakeHunger();
        _parameters.AddEnergy();
    }
}
