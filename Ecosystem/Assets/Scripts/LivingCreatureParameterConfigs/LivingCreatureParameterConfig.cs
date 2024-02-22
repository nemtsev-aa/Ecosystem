using UnityEngine;

public class LivingCreatureParameterConfig : ScriptableObject {
    [field: SerializeField] public float Value { get; private set; }
    [field: SerializeField] public float MaxValue { get; private set; }
}
