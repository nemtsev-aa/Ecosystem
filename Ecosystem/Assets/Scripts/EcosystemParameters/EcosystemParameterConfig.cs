using UnityEngine;

public class EcosystemParameterConfig : ScriptableObject {
    [SerializeField] protected EcosystemParameterVariants _variant;

    public EcosystemParameterVariants Variant => _variant;
}
