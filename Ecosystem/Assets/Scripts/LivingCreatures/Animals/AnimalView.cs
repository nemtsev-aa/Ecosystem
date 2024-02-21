using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimalView : LivingCreatureView {
    private const string IsIdling = "IsIdling";
    private const string IsDrowing = "IsDrowing";
    private const string IsEating = "IsEating";
    private const string IsWalking = "IsWalking";
    private const string IsRunning = "IsRunning";
    private const string IsReproducting = "IsReproducting";

    [SerializeField] private ParameterView _energyView;
    [SerializeField] private ParameterView _hungerView;

    public override void Init(LivingCreatureStateMachine stateMachine, LivingCreatureParameters parameters) {
        base.Init(stateMachine, parameters);
    }

    public override void CreateStatesDictionary(IReadOnlyList<IState> states) {
        _states = new Dictionary<IState, string> {
            {FindStateByType<IdlingState>(states), IsIdling },
            {FindStateByType<GrowingState>(states), IsDrowing },
            {FindStateByType<EatingState>(states), IsEating },
            {FindStateByType<WalkingState>(states), IsWalking },
            {FindStateByType<RunningState>(states), IsRunning },
            {FindStateByType<ReproductionState>(states), IsReproducting }
        };
    }

    public override void InitParameterViews(LivingCreatureParameters parameters) {
        var animalParameters = (AnimalParameters)parameters;

        _ageView.Init(animalParameters.Age);
        _growView.Init(animalParameters.Grow);
        _healthView.Init(animalParameters.Health);
        _energyView.Init(animalParameters.Energy);
        _hungerView.Init(animalParameters.Hunger);
        _reproductionView.Init(animalParameters.ReproductionDesire);
    }
}
