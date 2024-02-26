using System;
using UnityEngine;

public class EcosystemParameterConfig : ScriptableObject {
    [SerializeField] protected EcosystemParameterVariants _variant;

    public EcosystemParameterVariants Variant {
        get => _variant;
        set {
            _variant = value;
        }
    }

    public string GetText() {
        switch (_variant) {
            case EcosystemParameterVariants.ExtraLow:
                return "����� �����";

            case EcosystemParameterVariants.Low:
                return "�����";

            case EcosystemParameterVariants.Normal:
                return "�����";

            case EcosystemParameterVariants.High:
                return "������";

            case EcosystemParameterVariants.ExtraHigh:
                return "����� ������";

            default:
                throw new ArgumentException($"Invalid EcosystemParameterVariant: {_variant}");
        }
    }
    
    public float GetValue() {
        switch (_variant) {
            case EcosystemParameterVariants.ExtraLow:
                return 0.8f;

            case EcosystemParameterVariants.Low:
                return 0.9f;

            case EcosystemParameterVariants.Normal:
                return 1;

            case EcosystemParameterVariants.High:
                return 1.1f;

            case EcosystemParameterVariants.ExtraHigh:
                return 1.2f;

            default:
                throw new ArgumentException($"Invalid EcosystemParameterVariant: {_variant}");
        }
    }

}
