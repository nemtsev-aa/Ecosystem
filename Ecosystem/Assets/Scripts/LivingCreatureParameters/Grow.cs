using UnityEngine;

[CreateAssetMenu(fileName = nameof(Grow), menuName = "Configs/LivingCreatureParameters/" + nameof(Grow))]
public class Grow : LivingCreatureParameter {
    public Grow(float value, float maxValue) : base(value, maxValue) {
    }
}
