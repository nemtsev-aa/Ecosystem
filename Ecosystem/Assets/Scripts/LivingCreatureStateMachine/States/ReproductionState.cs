using System;
using UnityEngine;

public class ReproductionState : IState {
    public IStateSwitcher StateSwitcher { get; set; }

    protected readonly LivingCreatureStateMachineData Data;
    protected readonly LivingCreature LivingCreature;

    public ReproductionState(LivingCreatureStateMachineData data, LivingCreature livingCreature) {
        Data = data;
        LivingCreature = livingCreature;
    }

    public void Enter() {
        Debug.Log($"ReproductionState: Enter");
    }

    public void Exit() {
        Debug.Log($"ReproductionState: Exit");
    }

    public void Update() {
        if (LivingCreature is Animal) {
            StartAnimalReproduction();
        }

        if (LivingCreature is Plant) {
            StartPlantReproduction();
        }    
    }

    private void StartPlantReproduction() {
        Animal animal1 =  (Animal)LivingCreature;
        Animal animal2 = (Animal)Data.ReproductionTarget;

        Debug.Log($"StartPlantReproduction: {animal1} / {animal2}");
    }

    private void StartAnimalReproduction() {
        LivingCreature.Reproducted?.Invoke(LivingCreature);
    }

    private Vector2 GetRandomPosition() {
        float x = Mathf.Cos(UnityEngine.Random.Range(0, 360)) * Data.Radius;
        float y = Mathf.Sin(UnityEngine.Random.Range(0, 180)) * Data.Radius;
        
        return  new Vector2(x, y);
    }
}
