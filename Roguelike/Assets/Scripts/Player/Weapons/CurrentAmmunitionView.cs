using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurrentAmmunitionView : MonoBehaviour
{
    [SerializeField] private PlayerWeapons _playerWeapons;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Slider _slider;

    private Ammunition _ammunition;

    private void Awake()
    {
        _playerWeapons.WeaponChanged += OnWeaponChanged;
    }
    
    private void OnEnable()
    {
        TrySubscribe();
    }
    
    private void OnDisable()
    {
        TryUnsubscribe();
    }

    private void TrySubscribe()
    {
        if (_ammunition == null)
            return;;
        
        _ammunition.ValueChanged += OnValueChanged;
        _ammunition.MaxValueChanged += OnMaxValueChanged;
    }

    private void TryUnsubscribe()
    {
        if (_ammunition == null)
            return;;
        
        _ammunition.ValueChanged -= OnValueChanged;
        _ammunition.MaxValueChanged -= OnMaxValueChanged;
    }

    private void OnValueChanged(int newValue)
    {
        _slider.value = newValue;
        ChangeText();
    }

    private void OnMaxValueChanged(int newMaxValue)
    {
        _slider.maxValue = newMaxValue;
        ChangeText();
    }

    private void OnWeaponChanged(Ammunition ammunition)
    {
        TryUnsubscribe();
        _ammunition = ammunition;
        TrySubscribe();
        
        OnMaxValueChanged(_ammunition.MaxAmount);
        OnValueChanged(_ammunition.CurrentAmount);
    }

    private void ChangeText()
    {
        _text.text = $"{_slider.value} | {_slider.maxValue}";
    }
}
