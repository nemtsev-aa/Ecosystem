using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

[CreateAssetMenu(fileName = nameof(AnimalParameters), menuName = "Configs/LivingCreatureParameters/" + nameof(AnimalParameters))]
public class AnimalParameters : LivingCreatureParameters, IDisposable {
    [SerializeField] private List<LivingCreatureParameterConfig> _dynamicParameterConfigs;

    [Header("Static")]
    [SerializeField, Range(1, 100)] private float _damage;

    [Header("TimeDurations")]
    [SerializeField, Range(0, 1)] private float _eatingDuration;
    [SerializeField, Range(0, 1)] private float _hungerAppendSpeed;
    [SerializeField, Range(0, 1)] private float _energyDecreaseSpeed;
    [SerializeField, Range(0, 1)] private float _growingSpeed;
    [SerializeField, Range(0, 1)] private float _reproductionSpeed;

    private Animal _animal;
    private float _temperatureFactor;
    private float _humidityFactor;

    private Age _age;
    private Grow _grow;
    private Health _health;
    private float _maxHealth;
    private Energy _energy;
    private Hunger _hunger;
    private ReproductionDesire _reproductionDesire;

    private List<LivingCreatureParameterConfig> _dynamicParameters = new List<LivingCreatureParameterConfig>();

    [field: SerializeField] public LayerMask FoodLayer { get; private set; }
    [field: SerializeField] public LayerMask EnemyLayer { get; private set; }
    [field: SerializeField] public LayerMask ReproductionLayer { get; private set; }
    
    private float DeltaGrowing => _growingSpeed * _temperatureFactor * Time.deltaTime;
    private float DecreaseHunger => _hungerAppendSpeed * 5f * Time.deltaTime;
    private float AppendHunger => _hungerAppendSpeed * _temperatureFactor * Time.deltaTime;
    private float DecreaseEnergy => _energyDecreaseSpeed * _temperatureFactor * Time.deltaTime;
    private float AppendEnergy => _energyDecreaseSpeed * 10f * Time.deltaTime;
    private float DeltaReproduction => _reproductionSpeed * _temperatureFactor * Time.deltaTime;
    
    public Age Age => _age;
    public Health Health => _health;
    public Grow Grow => _grow;
    public Energy Energy => _energy;
    public Hunger Hunger => _hunger;
    public ReproductionDesire ReproductionDesire => _reproductionDesire;
    public float Damage => _damage;
    public float EatingDuration => _eatingDuration;
    public bool IsMoved { get; set; }

    public void Init(Animal animal, EcosystemParametersConfig ecosystemConfig) {
        _animal = animal;

        _temperatureFactor = ecosystemConfig.TemperatureConfig.GetValue();
        _humidityFactor = ecosystemConfig.HumidityConfig.GetValue();

        _health = new Health(GetClone<HealthConfig>());
        _health.MaxValue += _temperatureFactor * _humidityFactor;

        _age = new Age(GetClone<AgeConfig>());
        _age.MaxValue += _temperatureFactor;

        _grow = new Grow(GetClone<GrowConfig>());
        _energy = new Energy(GetClone<EnergyConfig>());
        _hunger = new Hunger(GetClone<HungerConfig>());
        _reproductionDesire = new ReproductionDesire(GetClone<ReproductionDesireConfig>());
        _maxHealth = _health.MaxValue;

        AddSubscribers();
    }

    private void AddSubscribers() {
        _age.HasBeenMax += OnAgeHasBeenMax;
        _health.HasBeenMin += OnHealthHasBeenMin;
        _grow.HasBeenMax += OnGrowHasBeenMax;
        _energy.HasBeenMin += OnEnergyHasBeenMin;
        _hunger.HasBeenMax += OnHungerHasBeenMax;
        _reproductionDesire.HasBeenMax += OnReproductionDesireHasBeenMax;
    }

    private void RemoveSubscribers() {
        _age.HasBeenMax -= OnAgeHasBeenMax;
        _health.HasBeenMin -= OnHealthHasBeenMin;
        _grow.HasBeenMax -= OnGrowHasBeenMax;
        _energy.HasBeenMin -= OnEnergyHasBeenMin;
        _hunger.HasBeenMin -= OnHungerHasBeenMin;
        _reproductionDesire.HasBeenMax -= OnReproductionDesireHasBeenMax;
    }

    private T GetClone<T>() where T : LivingCreatureParameterConfig {
        var config = (T)_dynamicParameterConfigs.FirstOrDefault(parameter => parameter is T);
        return Instantiate(config);
    }

    private void OnHungerHasBeenMax() {
        TakeEnergy();
    }

    private void OnHungerHasBeenMin() {
        AddDesireReproduction();
    }

    private void OnEnergyHasBeenMin() {
         TakeDamage(_damage / 10f);
    }

    private void OnAgeHasBeenMax() => _animal.Destroyed?.Invoke(_animal);

    private void OnHealthHasBeenMin() => _animal.Destroyed?.Invoke(_animal);

    private void OnGrowHasBeenMax() {
        if (_age.Value >= _age.MaxValue)
            return;

        _grow.Value = 0f;
        _age.Value++;

        _animal.Growed?.Invoke(_animal);
    }

    private void OnReproductionDesireHasBeenMax() {
        _reproductionDesire.Value = 0f;
        _animal.Reproducted?.Invoke(_animal);
    }

    public void TakeDamage(float damage) {
        _health.Value -= damage;
    }

    public void AddHealing(float healing) {
        _health.Value += healing;
    }

    public void AddEnergy() {
        _energy.Value = 1f;
    }

    public void TakeEnergy() {
        if (_energy.Value > DecreaseEnergy)
            _energy.Value -= DecreaseEnergy;                
    }

    public void AddHunger() {
        _hunger.Value += AppendHunger;
    }

    public void TakeHunger() {
        _hunger.Value -= DecreaseHunger;
        AddEnergy();
    }

    public void AddDesireReproduction() {
        _reproductionDesire.Value += DeltaReproduction;
    }

    public void TakeDesireReproduction() {
        _reproductionDesire.Value -= DeltaReproduction;

        TakeEnergy();
    }
    
    public void AddGrowing() {
        if (_energy.Value >= 0.2f) {
            _grow.Value += DeltaGrowing;
        }
    }

    public void Dispose() {
        RemoveSubscribers();
    }
}
