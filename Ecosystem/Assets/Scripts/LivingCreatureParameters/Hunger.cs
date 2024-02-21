using UnityEngine;

[CreateAssetMenu(fileName = nameof(Hunger), menuName = "Configs/LivingCreatureParameters/" + nameof(Hunger))]
public class Hunger : LivingCreatureParameter {
    public Hunger(float value, float maxValue) : base(value, maxValue) {
    }
}
