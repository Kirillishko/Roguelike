using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class AmmunitionView : MonoBehaviour
{
    [SerializeField] private Ammunition _ammunition;
    
    private Slider _slider;

    private void Start()
    {
        _slider = GetComponent<Slider>();
        
        _slider.maxValue = _ammunition.MaxAmount;
        _slider.value = _ammunition.CurrentAmount;
    }

    private void OnEnable()
    {
        _ammunition.ValueChanged += OnValueChanged;
        _ammunition.MaxValueChanged += OnMaxValueChanged;
    }

    private void OnDisable()
    {
        _ammunition.ValueChanged -= OnValueChanged;
        _ammunition.MaxValueChanged -= OnMaxValueChanged;
    }

    private void OnValueChanged(int newValue)
    {
        _slider.value = newValue;
    }

    private void OnMaxValueChanged(int newMaxValue)
    {
        _slider.maxValue = newMaxValue;
    }
}
