using System;
using UnityEngine;

public class LivingCreatureStateMachineData {
    private float _speed;

    public float Radius { get; set; } = 2f;
    
    public LivingCreature FoodTarget { get; set; }
    public LivingCreature ReproductionTarget { get; set; }
    public Vector2 TargetPosition { get; set; }

    public float Speed {
        get => _speed * Time.deltaTime;
        set {
            if(value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));

            _speed = value;
        }
    }
}
