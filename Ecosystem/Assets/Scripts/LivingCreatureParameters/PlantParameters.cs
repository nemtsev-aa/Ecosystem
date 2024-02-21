using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(PlantParameters), menuName = "Configs/LivingCreatureParameters/" + nameof(PlantParameters))]
public class PlantParameters : LivingCreatureParameters {
    [SerializeField] private List<LivingCreatureParameter> _dynamicParameterConfigs;

    [Header("TimeDurations")]
    [SerializeField, Range(1, 2000)] private float _growingEndTime;
    [SerializeField, Range(1, 2000)] private float _reproductionEndTime;

    private Plant _plant;
    private Age _age;
    private Grow _grow;
    private Health _health;
    private float _maxHealth;

    private ReproductionDesire _reproductionDesire;
    
    [field: SerializeField] public LayerMask ReproductionLayer { get; private set; }

    private float DeltaGrowing => Time.deltaTime / _growingEndTime;
    private float DeltaReproduction => Time.deltaTime / _reproductionEndTime;

    public Age Age => _age;
    public Health Health => _health;
    public Grow Grow => _grow;
    public ReproductionDesire ReproductionDesire => _reproductionDesire;

    public bool IsDead { get; set; }

    public void Init(Plant plant) {
        _plant = plant;

        _health = GetClone<Health>();
        _age = GetClone<Age>();
        _grow = GetClone<Grow>();
        _reproductionDesire = GetClone<ReproductionDesire>();

        _maxHealth = _health.MaxValue;
    }

    public void TakeDamage(float damage) {
        _health.Value -= damage;

        if (_health.Value <= 0)
            _plant.Destroyed?.Invoke(_plant);
    }

    public void AddHealing(float healing) {
        _health.Value += healing;
    }

    public void AddDesireReproduction() {
        _reproductionDesire.Value += DeltaReproduction;
    }

    public void TakeDesireReproduction() {
        _reproductionDesire.Value -= DeltaReproduction;
    }
    
    public void AddGrowing() {
        _grow.Value += DeltaGrowing;

        if (_grow.Value >= 1) {
            _grow.Value = 0f;

            _age.Value ++;
        }
    }

    private T GetClone<T>() where T : LivingCreatureParameter {
        var config = (T)_dynamicParameterConfigs.FirstOrDefault(parameter => parameter is T);
        return Instantiate(config);
    }

}
