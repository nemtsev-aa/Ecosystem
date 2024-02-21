using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(LivingCreaturePrefabs), menuName = "Configs/Prefabs/" + nameof(LivingCreaturePrefabs))]
public class LivingCreaturePrefabs : ScriptableObject {
    [field: SerializeField] public List<LivingCreature> Prefabs { get; private set; }
}
