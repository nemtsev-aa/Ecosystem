using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingCreatureSpawner : MonoBehaviour {
    private const float SpawnCooldown = 0.1f;

    public event Action<int> AnimalCountChanged;
    public event Action<int> PlantCountChanged;

    [SerializeField] private Transform _animalSpawnField;
    [SerializeField] private Transform _plantSpawnField;

    [SerializeField, Range(0, 100)] private int _animalSpawnCount;
    [SerializeField, Range(0, 100)] private int _plantSpawnCount;

    private AnimalConfig _animalConfig;
    private PlantConfig _plantConfig;
    private EcosystemParametersConfig _ecosystemParametersConfig;

    private LivingCreatureFactory _factory;
    private List<LivingCreature> _livingCreatures;
    private List<Animal> _animals;
    private List<Plant> _plants;

    public void Init(LivingCreatureFactory factory, LivingCreatureConfigs configs, EcosystemParametersConfig ecosystemParametersConfig) {
        _factory = factory;

        _animalConfig = configs.AnimalConfig;
        _plantConfig = configs.PlantConfig;
        _ecosystemParametersConfig = ecosystemParametersConfig;

        _livingCreatures = new List<LivingCreature>();
        _animals = new List<Animal>();
        _plants = new List<Plant>();

        StartCoroutine(SpawnAnimals(_animalSpawnCount));
        StartCoroutine(SpawnPlants(_plantSpawnCount));
    }

    public void CreateAnimal() {
        Animal animal = _factory.Get<Animal>(_animalConfig, transform);
        animal.transform.position = GetRanfomPointInSpawnField(_animalSpawnField);
        animal.Destroyed += OnDestroyed;
        animal.Reproducted += OnReproducted;

        animal.Init(_animalConfig, _ecosystemParametersConfig);

        _livingCreatures.Add(animal);
        _animals.Add(animal);

        AnimalCountChanged?.Invoke(_animals.Count);
    }

    public void CreatePlant() {
        Plant plant = _factory.Get<Plant>(_plantConfig, transform);
        plant.transform.position = GetRanfomPointInSpawnField(_plantSpawnField);
        plant.Destroyed += OnDestroyed;
        plant.Reproducted += OnReproducted;

        plant.Init(_plantConfig, _ecosystemParametersConfig);

        _livingCreatures.Add(plant);
        _plants.Add(plant);

        PlantCountChanged?.Invoke(_plants.Count);
    }

    public void ShowViews(bool status) {
        foreach (var iLivingCreature in _livingCreatures) {
            iLivingCreature.ShowView(status);
        }
    }

    public void Reset() {
        foreach (var iCoin in _livingCreatures) {
            Destroy(iCoin.gameObject);
        }

        _livingCreatures.Clear();
        _animals.Clear();
        _plants.Clear();
    }

    public IEnumerator SpawnAnimals(int spawnCount) {
        for (int i = 0; i < spawnCount; i++) {
            CreateAnimal();
            yield return new WaitForSeconds(SpawnCooldown);
        }
    }

    public IEnumerator SpawnPlants(int spawnCount) {
        for (int i = 0; i < spawnCount; i++) {
            CreatePlant();

            yield return new WaitForSeconds(SpawnCooldown);
        }
    }

    private Vector2 GetRanfomPointInSpawnField(Transform field) {
        float x = UnityEngine.Random.Range(-0.5f, 0.5f);
        float y = UnityEngine.Random.Range(-0.5f, 0.5f);
        float z = 0;

        return field.TransformPoint(x, y, z);
    }

    private void OnDestroyed(LivingCreature creature) {
        if (creature is Animal) {
            Animal animal = (Animal)creature;
            _animals.Remove(animal);
            AnimalCountChanged?.Invoke(_animals.Count);
        }

        if (creature is Plant) {
            Plant plant = (Plant)creature;
            _plants.Remove(plant);
            PlantCountChanged?.Invoke(_plants.Count);
        }

        creature.Destroyed -= OnDestroyed;
        _livingCreatures.Remove(creature);
        Destroy(creature.gameObject);
    }

    private void OnReproducted(LivingCreature livingCreature) {
        if (livingCreature is Animal) {
            CreateAnimal();
        }

        if (livingCreature is Plant) {
            CreatePlant();
        }
    }
}
