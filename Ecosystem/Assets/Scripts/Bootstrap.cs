using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour {
    [SerializeField] private LivingCreatureFactory _factory;
    [SerializeField] private LivingCreaturePrefabs _prefabs;
    [SerializeField] private LivingCreatureSpawner _spawner;

    //[SerializeField] private List<Plant> _plants;
    //[SerializeField] private PlantConfig _plantConfig;

    private void Start() {
        _factory.Init(_prefabs);
        _spawner.Init(_factory);

        //foreach (var iPlant in _plants) {
        //    iPlant.Init(_plantConfig);
        //}
    }
}
