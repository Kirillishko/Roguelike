using UnityEngine;

public class Ammunition
{
    private AmmoType _ammoType;
    private int _currentAmount;
    private int _maxAmount;

    public int CurrentAmount => _currentAmount;

    public Ammunition(AmmoType ammoType, int currentAmount, int maxAmount)
    {
        _ammoType = ammoType;
        _currentAmount = currentAmount;
        _maxAmount = maxAmount;
    }

    public void Add(int ammoAmount)
    {
        _currentAmount += ammoAmount;

        _currentAmount = Mathf.Max(_currentAmount, _maxAmount);
    }

    public void SetMaxAmount(int value)
    {
        _maxAmount = value;

        if (_currentAmount > _maxAmount)
            _currentAmount = _maxAmount;
    }
}
