using System;
using UnityEngine;

public abstract class LivingCreature : MonoBehaviour {
    public Action<LivingCreature> Growed;
    public Action<LivingCreature> Destroyed;
    public Action<LivingCreature> Reproducted;

    protected LivingCreatureStateMachine StateMachine { get; set; }
    protected EcosystemParametersConfig EcosystemConfig { get; set; }
    
    public abstract void Init(LivingCreatureConfig config, EcosystemParametersConfig ecosystemParametersConfig);
    public abstract void ShowView(bool status);

    private void Update() {
        if (StateMachine != null)
            StateMachine.Update();
    }
}
