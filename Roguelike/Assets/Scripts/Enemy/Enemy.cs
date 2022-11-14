using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Action Die;
    public Action<int, Vector3> Damaged;

    [SerializeField] private int _health;
    [SerializeField] private int _armor;
    [SerializeField] private Player _target;

    public Player Target => _target;

    public void Init(Player player) => _target = player;

    public void TakeDamage(int damage)
    {
        if (_health <= 0)
            return;
        
        int realDamage = damage - _armor;
        realDamage = Mathf.Max(realDamage, 0);
        
        _health -= realDamage;
        Damaged?.Invoke(damage, transform.position);

        if (_health <= 0)
            Die?.Invoke();
    }
}
