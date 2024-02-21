using UnityEngine;

[CreateAssetMenu(fileName = nameof(Age), menuName = "Configs/LivingCreatureParameters/" + nameof(Age))]
public class Age : LivingCreatureParameter {
    public Age(float value, float maxValue) : base(value, maxValue) {
    }
}
