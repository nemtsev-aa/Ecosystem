using UnityEngine;

[CreateAssetMenu(fileName = nameof(Energy), menuName = "Configs/LivingCreatureParameters/" + nameof(Energy))]
public class Energy : LivingCreatureParameter {
    public Energy(float value, float maxValue) : base(value, maxValue) {
    }
}
