using System.Collections.Generic;

public class PlantView : LivingCreatureView {
    private const string IsIdling = "IsIdling";
    private const string IsDrowing = "IsDrowing";
    private const string IsReproducting = "IsReproducting";

    public override void Init(LivingCreatureStateMachine stateMachine, LivingCreatureParameters parameters) {
        base.Init(stateMachine, parameters);
    }

    public override void CreateStatesDictionary(IReadOnlyList<IState> states) {
        _states = new Dictionary<IState, string> {
            {FindStateByType<PlantIdlingState>(states), IsDrowing },
            {FindStateByType<GrowingState>(states), IsDrowing },
            {FindStateByType<ReproductionState>(states), IsReproducting }
        };
    }

    public override void InitParameterViews(LivingCreatureParameters parameters) {
        var plantParameters = (PlantParameters)parameters;

        _ageView.Init(plantParameters.Age);
        _growView.Init(plantParameters.Grow);
        _healthView.Init(plantParameters.Health);
        _reproductionView.Init(plantParameters.ReproductionDesire);
    }
}
