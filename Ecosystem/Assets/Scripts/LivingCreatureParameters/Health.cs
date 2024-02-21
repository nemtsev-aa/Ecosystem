using UnityEngine;

[CreateAssetMenu(fileName = nameof(Health), menuName = "Configs/LivingCreatureParameters/" + nameof(Health))]
public class Health : LivingCreatureParameter {
    public Health(float value, float maxValue) : base(value, maxValue) {
    }
}
