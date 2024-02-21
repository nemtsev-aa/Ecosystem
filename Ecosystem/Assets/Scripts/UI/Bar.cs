using UnityEngine;
using UnityEngine.UI;

public abstract class Bar : MonoBehaviour {
    [SerializeField] protected Image Filler;

    protected abstract void OnValueChanged(float value);
}
