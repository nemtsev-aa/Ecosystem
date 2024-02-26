using UnityEngine;

[CreateAssetMenu(fileName = nameof(LivingCreatureConfigs), menuName = "Configs/LivingCreatureParameters/" + nameof(LivingCreatureConfigs))]
public class LivingCreatureConfigs : ScriptableObject {
    [field: SerializeField] public AnimalConfig AnimalConfig { get; private set; }
    [field: SerializeField] public PlantConfig PlantConfig { get; private set; }
}
