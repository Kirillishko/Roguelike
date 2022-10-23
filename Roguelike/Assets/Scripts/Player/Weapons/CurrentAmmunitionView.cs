using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurrentAmmunitionView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _value;
    [SerializeField] private PlayerWeapons _playerWeapons;

    private Ammunition _ammunition;
    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();

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
    }

    private void OnMaxValueChanged(int newMaxValue)
    {
        _slider.maxValue = newMaxValue;
    }

    private void OnWeaponChanged(Ammunition ammunition)
    {
        TryUnsubscribe();
        _ammunition = ammunition;
        TrySubscribe();
        
        OnMaxValueChanged(_ammunition.MaxAmount);
        OnValueChanged(_ammunition.CurrentAmount);
    }
}
