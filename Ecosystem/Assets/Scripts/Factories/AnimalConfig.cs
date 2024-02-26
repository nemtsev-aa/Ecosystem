using UnityEngine;

[CreateAssetMenu(fileName = nameof(AnimalConfig), menuName = "Configs/LivingCreature/" + nameof(AnimalConfig))]
public class AnimalConfig : LivingCreatureConfig {
    [field: SerializeField] public float Speed { get; private set; }
}
