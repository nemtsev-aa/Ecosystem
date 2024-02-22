using UnityEngine;

public class EcosystemParameterConfig : ScriptableObject {
    [SerializeField] protected EcosystemParameterVariants _variant;

    public EcosystemParameterVariants Variant {
        get => _variant;
        set {
            _variant = value;
        }
    }

}
