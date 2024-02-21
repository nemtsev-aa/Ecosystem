using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

[CreateAssetMenu(fileName = nameof(AnimalParameters), menuName = "Configs/LivingCreatureParameters/" + nameof(AnimalParameters))]
public class AnimalParameters : LivingCreatureParameters {
    [SerializeField] private List<LivingCreatureParameter> _dynamicParameterConfigs;

    [Header("Static")]
    [SerializeField, Range(1, 100)] private float _damage;

    [Header("TimeDurations")]
    [SerializeField, Range(1, 20)] private float _eatingDuration;
    [SerializeField, Range(1, 200)] private float _hungerEndTime;
    [SerializeField, Range(1, 200)] private float _energyEndTime;
    [SerializeField, Range(1, 2000)] private float _growingEndTime;
    [SerializeField, Range(1, 2000)] private float _reproductionEndTime;

    private Age _age;
    private Grow _grow;
    private Animal _animal;
    private Health _health;
    private float _maxHealth;
    private Energy _energy;
    private Hunger _hunger;
    private ReproductionDesire _reproductionDesire;
    private List<LivingCreatureParameter> _dynamicParameters = new List<LivingCreatureParameter>();

    [field: SerializeField] public LayerMask FoodLayer { get; private set; }
    [field: SerializeField] public LayerMask EnemyLayer { get; private set; }
    [field: SerializeField] public LayerMask ReproductionLayer { get; private set; }
    
    private float DeltaGrowing => Time.deltaTime / _growingEndTime;
    private float DeltaHunger => Time.deltaTime / _hungerEndTime;
    private float DeltaEnergy => Time.deltaTime / _energyEndTime;
    private float DeltaReproduction => Time.deltaTime / _reproductionEndTime;
    
    public Age Age => _age;
    public Health Health => _health;
    public Grow Grow => _grow;
    public Energy Energy => _energy;
    public Hunger Hunger => _hunger;
    public ReproductionDesire ReproductionDesire => _reproductionDesire;
    public float Damage => _damage;
    public float EatingDuration => _eatingDuration;

    public bool IsMoved { get; set; }
    public bool IsDead { get; set; } = false;

    public void Init(Animal animal) {
        _animal = animal;

        _health = GetClone<Health>();
        _age = GetClone<Age>();
        _grow = GetClone<Grow>();
        _energy = GetClone<Energy>();
        _hunger = GetClone<Hunger>();
        _reproductionDesire = GetClone<ReproductionDesire>();

        _maxHealth = _health.MaxValue;
    }

    public void TakeDamage(float damage) {
        _health.Value -= damage;

        if (_health.Value <= 0) {
            _animal.Destroyed?.Invoke(_animal);
        }  
    }

    public void AddHealing(float healing) {
        _health.Value += healing;

        if (_health.Value >= _maxHealth && _energy.Value < 1f)
            AddEnergy();
    }

    public void AddEnergy() {
        if (_energy.Value >= 1f)
            _health.Value += DeltaEnergy;
        else
            _energy.Value += DeltaEnergy * 2f;
    }

    public void TakeEnergy() {
        if (_energy.Value > DeltaEnergy)
            _energy.Value -= DeltaEnergy;
        else
            _energy.Value = 0f;

        if (_energy.Value == 0f) {
            TakeDamage(DeltaEnergy);
        }
    }

    public void AddHunger() {
        _hunger.Value += DeltaHunger;

        if (_hunger.Value >= 1) {
            _hunger.Value = 1f;
            TakeEnergy();
        }
    }

    public void TakeHunger() {
        AddEnergy();

        _hunger.Value -= DeltaHunger * 10f;
    }

    public void AddDesireReproduction() {
        _reproductionDesire.Value += DeltaReproduction;
    }

    public void TakeDesireReproduction() {
        _reproductionDesire.Value -= DeltaReproduction;

        TakeEnergy();
    }
    
    public void AddGrowing() {
        _grow.Value += DeltaGrowing;

        if (_grow.Value >= 1) {
            _grow.Value = 0f;

            _age.Value++;

            if (_age.Value >= _age.MaxValue)
                _animal.Destroyed?.Invoke(_animal);
        }
    }

    private T GetClone<T>() where T : LivingCreatureParameter {
        var config = (T)_dynamicParameterConfigs.FirstOrDefault(parameter => parameter is T);
        return Instantiate(config);
    }
}
