using System;
using UnityEngine;

public class Ammunition : MonoBehaviour
{
    public Action<int> ValueChanged;
    public Action<int> MaxValueChanged;
    
    [SerializeField, Min(0)] private int _currentAmount;
    [SerializeField, Min(1)] private int _maxAmount;

    private void OnValidate()
    {
        if (_currentAmount > _maxAmount)
            _maxAmount = _currentAmount;
    }
    
    public int CurrentAmount => _currentAmount;
    public int MaxAmount => _maxAmount;

    public void Add(int ammoAmount)
    {
        _currentAmount += ammoAmount;

        _currentAmount = Mathf.Min(_currentAmount, _maxAmount);
        
        ValueChanged?.Invoke(_currentAmount);
    }

    public void SetMaxAmount(int value)
    {
        _maxAmount = value;

        if (_currentAmount > _maxAmount)
            _currentAmount = _maxAmount;
        
        MaxValueChanged?.Invoke(_maxAmount);
    }
}
