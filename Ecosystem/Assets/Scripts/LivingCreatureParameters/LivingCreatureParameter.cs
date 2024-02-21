using System;
using UnityEngine;

public class LivingCreatureParameter : ScriptableObject {
    public event Action<float> ValueChanged;

    protected float _startFilling;
    [SerializeField] protected float _value;
    [SerializeField] protected float _maxValue;

    public LivingCreatureParameter(float value, float maxValue) {
        _maxValue = maxValue;
        _value = value;
        _startFilling = value / maxValue;
    }

    public float Value {
        get => _value;
        set {

            if (value < 0)
                throw new ArgumentOutOfRangeException($"Invalid argument value: {value}");

            _value = value;
            ValueChanged?.Invoke(_value / _maxValue);
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
}
