using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Plant : LivingCreature {
    private List<IState> _states;
    private LivingCreatureStateMachineData _data;

    [field: SerializeField] public PlantParameters Parameters { get; private set; }
    [field: SerializeField] public PlantView View { get; private set; }

    public PlantConfig Config { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }
    public bool IsMoved { get; set; }

    public override void Init(LivingCreatureConfig config) {
        Config = (PlantConfig)config;

        PlantParameters parameters = Instantiate(Parameters);
        Parameters = parameters;
        Parameters.Init(this);

        Rigidbody = GetComponent<Rigidbody2D>();

        _data = new LivingCreatureStateMachineData();
        _states = new List<IState>() {
            new PlantIdlingState(_data, this),
            new GrowingState(_data, this),
            new ReproductionState(_data,this),
        };

        StateMachine = new LivingCreatureStateMachine(this, _states);
        View.Init(StateMachine, Parameters);
    }

    public override void ShowView(bool status) {
        View.ShowCanvas(status);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Environment")) {
            Debug.Log(this.gameObject.name);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected() {
        if (_data != null) {
            Handles.color = Color.green;
            Handles.DrawWireDisc(transform.position, Vector3.forward, _data.Radius);
            Handles.color = Color.red;
            Handles.DrawSolidDisc(_data.TargetPosition, Vector3.forward, 0.1f);
        }
    }
#endif
}
