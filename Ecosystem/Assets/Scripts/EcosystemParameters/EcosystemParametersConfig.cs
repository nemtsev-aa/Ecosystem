using UnityEngine;
[CreateAssetMenu(fileName = nameof(EcosystemParametersConfig), menuName = "Configs/EcosystemParameters/" + nameof(EcosystemParametersConfig))]
public class EcosystemParametersConfig : ScriptableObject {
    [field: SerializeField] public HumidityConfig HumidityConfig { get; set; }
    [field: SerializeField] public TemperatureConfig TemperatureConfig { get; set; }
}
