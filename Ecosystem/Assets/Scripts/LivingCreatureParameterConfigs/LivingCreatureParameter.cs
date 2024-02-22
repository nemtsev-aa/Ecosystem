using System;

[Serializable]
public class LivingCreatureParameter {
    public event Action<float> ValueChanged;
    public event Action HasBeenMax;
    public event Action HasBeenMin;

    private float _startFilling;
    private float _maxValue;
    private float _value;

    public LivingCreatureParameter(float value, float maxValue) {
        _maxValue = maxValue;
        _value = value;
        _startFilling = value / maxValue;
    }

    public LivingCreatureParameter(LivingCreatureParameterConfig config) {
        _value = config.Value;
        _maxValue = config.MaxValue;
        _startFilling = _value / _maxValue;
    }

    public float Value {
        get => _value;
        set {

            if (value < 0)
                throw new ArgumentOutOfRangeException($"Invalid argument value: {value}");

            _value = value;

            if (_value <= 0) {
                HasBeenMin?.Invoke();
                return;
            }

            if (_value >= _maxValue) {
                HasBeenMax?.Invoke();
                return;
            }

            if (_value > 0) {
                ValueChanged?.Invoke(_value / _maxValue);
                return;
            }
        }
    }

    public float MaxValue {
        get => _maxValue;
        set {

            if (value < 0)
                throw new ArgumentOutOfRangeException($"Invalid argument value: {value}");

            _maxValue = value;
        }
    }

    public float StartFilling => _startFilling;
    public float CurrentFilling => _value / _maxValue;
}
