using TMPro;
using UnityEngine;

public class EcosystemParametersPanel : UIPanel {
    [SerializeField] private TextMeshProUGUI _temperatureValueText;
    [SerializeField] private TextMeshProUGUI _humidityValueText;

    public void Init(EcosystemParametersConfig config) {
        _temperatureValueText.text = config.TemperatureConfig.GetText();
        _humidityValueText.text = config.HumidityConfig.GetText();
    }
}
