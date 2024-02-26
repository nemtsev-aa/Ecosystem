using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(PlantParameters), menuName = "Configs/LivingCreatureParameters/" + nameof(PlantParameters))]
public class PlantParameters : LivingCreatureParameters, IDisposable {
    [SerializeField] private List<LivingCreatureParameterConfig> _dynamicParameterConfigs;

    [Header("TimeDurations")]
    [SerializeField, Range(0, 1f)] private float _growingSpeed;
    [SerializeField, Range(0, 1f)] private float _reproductionSpeed;

    private Plant _plant;
    private float _temperatureFactor;
    private float _humidityFactor;

    private Health _health;
    private Age _age;
    private Grow _grow;
    private ReproductionDesire _reproductionDesire;
    private float _maxHealth;

    private float _deltaGrowing => _growingSpeed * _temperatureFactor * _humidityFactor * Time.deltaTime;
    private float _deltaReproduction => _reproductionSpeed * _temperatureFactor * _humidityFactor * Time.deltaTime;

    public Age Age => _age;
    public Health Health => _health;
    public Grow Grow => _grow;
    public ReproductionDesire ReproductionDesire => _reproductionDesire;

    public void Init(Plant plant, EcosystemParametersConfig ecosystemConfig) {
        _plant = plant;

        _temperatureFactor = ecosystemConfig.TemperatureConfig.GetValue();
        _humidityFactor = ecosystemConfig.HumidityConfig.GetValue();

        _health = new Health(GetClone<HealthConfig>());
        _health.MaxValue += _temperatureFactor * _humidityFactor;

        _age = new Age(GetClone<AgeConfig>());
        _age.MaxValue += _temperatureFactor * _humidityFactor;

        _grow = new Grow(GetClone<GrowConfig>());
        _reproductionDesire = new ReproductionDesire(GetClone<ReproductionDesireConfig>());
        _maxHealth = _health.MaxValue;

        AddSubscribers();
    }

    public void TakeDamage(float damage) => _health.Value -= damage;

    public void AddHealing(float healing) => _health.Value += healing;

    public void AddDesireReproduction() =>
        _reproductionDesire.Value += _deltaReproduction;


    public void TakeDesireReproduction() =>
        _reproductionDesire.Value -= _deltaReproduction;


    public void AddGrowing() {
        if (_age.CurrentFilling < 1f) 
            _grow.Value += _deltaGrowing;
    }
  
    public void Dispose() {
        RemoveSubscribers();
    }


    private T GetClone<T>() where T : LivingCreatureParameterConfig {
        var config = (T)_dynamicParameterConfigs.FirstOrDefault(parameter => parameter is T);
        return Instantiate(config);
    }

    private void AddSubscribers() {
        _age.HasBeenMax += OnAgeHasBeenMax;
        _health.HasBeenMin += OnHealthHasBeenMin;
        _grow.HasBeenMax += OnGrowHasBeenMax;
        _reproductionDesire.HasBeenMax += OnReproductionDesireHasBeenMax;
    }

    private void RemoveSubscribers() {
        _age.HasBeenMax -= OnAgeHasBeenMax;
        _health.HasBeenMin -= OnHealthHasBeenMin;
        _grow.HasBeenMax -= OnGrowHasBeenMax;
        _reproductionDesire.HasBeenMax -= OnReproductionDesireHasBeenMax;
    }

    private void OnAgeHasBeenMax() => _plant.Destroyed?.Invoke(_plant);

    private void OnHealthHasBeenMin() => _plant.Destroyed?.Invoke(_plant);

    private void OnGrowHasBeenMax() {
        if (_age.Value >= _age.MaxValue) 
            return;

        _grow.Value = 0f;

        _age.Value++;
        _plant.Growed?.Invoke(_plant);
    }

    private void OnReproductionDesireHasBeenMax() {
        _reproductionDesire.Value = 0f;
        _plant.Reproducted?.Invoke(_plant);
    }
}
