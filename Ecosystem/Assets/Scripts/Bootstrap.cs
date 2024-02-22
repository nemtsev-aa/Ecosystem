using UnityEngine;
using Zenject;

public class Bootstrap : MonoBehaviour {
    [SerializeField] private UIManager _uIManager;
    [SerializeField] private DialogFactory _dialogFactory;
    [SerializeField] private UICompanentsFactory _companentsFactory;

    [SerializeField] private LivingCreatureFactory _factory;
    [SerializeField] private LivingCreaturePrefabs _prefabs;
    [SerializeField] private LivingCreatureSpawner _spawner;

    private Logger _logger;

    [Inject]
    public void Construct(Logger logger) {
        _logger = logger;
    }

    private void Start() {
        _logger.Log("Начало метода [Bootstrapper: Start]");

        _factory.Init(_prefabs);
        _spawner.Init(_factory);
        _uIManager.Init(_spawner);

        _logger.Log("Конец метода [Bootstrapper: Start]");
    }
}
