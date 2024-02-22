using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingCreatureSpawner : MonoBehaviour {
    private const float SpawnCooldown = 0.1f;

    [SerializeField] private Transform _animalSpawnField;
    [SerializeField] private Transform _plantSpawnField;

    [SerializeField, Range(0, 100)] private int _animalSpawnCount;
    [SerializeField, Range(0, 100)] private int _plantSpawnCount;

    [SerializeField] private AnimalConfig _animalConfig;
    [SerializeField] private PlantConfig _plantConfig;

    private LivingCreatureFactory _factory;
    private List<LivingCreature> _livingCreatures;

    public void Init(LivingCreatureFactory factory) {
        _factory = factory;

        _livingCreatures = new List<LivingCreature>();

        StartCoroutine(SpawnAnimals(_animalSpawnCount));
        StartCoroutine(SpawnPlants(_plantSpawnCount));
    }

    public void CreateAnimal() {
        Animal animal = _factory.Get<Animal>(_animalConfig, transform);
        animal.transform.position = GetRanfomPointInSpawnField(_animalSpawnField);
        animal.Destroyed += OnDestroyed;
        animal.Reproducted += OnReproducted;

        animal.Init(_animalConfig);

        _livingCreatures.Add(animal);
    }

    public void CreatePlant() {
        Plant plant = _factory.Get<Plant>(_plantConfig, transform);
        plant.transform.position = GetRanfomPointInSpawnField(_plantSpawnField);
        plant.Destroyed += OnDestroyed;
        plant.Reproducted += OnReproducted;

        plant.Init(_plantConfig);

        _livingCreatures.Add(plant);
    }

    [ContextMenu("ShowViews")]
    public void ShowViews() {
        foreach (var iLivingCreature in _livingCreatures) {
            iLivingCreature.ShowView(true);
        }
    }

    [ContextMenu("HideViews")]
    public void HideViews() {
        foreach (var iLivingCreature in _livingCreatures) {
            iLivingCreature.ShowView(false);
        }
    }

    public void Reset() {
        foreach (var iCoin in _livingCreatures) {
            Destroy(iCoin.gameObject);
        }

        _livingCreatures.Clear();
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
        float x = Random.Range(-0.5f, 0.5f);
        float y = Random.Range(-0.5f, 0.5f);
        float z = 0;

        return field.TransformPoint(x, y, z);
    }

    private void OnDestroyed(LivingCreature creature) {
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
