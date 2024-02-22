using UnityEngine;

public class Bootstrap : MonoBehaviour {
    [SerializeField] private UIManager _uIManager;
    [SerializeField] private DialogFactory _dialogFactory;
    [SerializeField] private UICompanentsFactory _companentsFactory;

    [SerializeField] private LivingCreatureFactory _factory;
    [SerializeField] private LivingCreaturePrefabs _prefabs;
    [SerializeField] private LivingCreatureSpawner _spawner;

    private void Start() {
        Logger.Instance.Log("Начало метода [Bootstrapper: Start]");

        _uIManager.Init(_companentsFactory, _dialogFactory);

        _factory.Init(_prefabs);
        _spawner.Init(_factory);

        Logger.Instance.Log("Конец метода [Bootstrapper: Start]");
    }

}
