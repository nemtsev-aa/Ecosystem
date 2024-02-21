using UnityEngine;

[CreateAssetMenu(fileName = nameof(ReproductionDesire), menuName = "Configs/LivingCreatureParameters/" + nameof(ReproductionDesire))]
public class ReproductionDesire : LivingCreatureParameter {
    public ReproductionDesire(float value, float maxValue) : base(value, maxValue) {
    }
}
