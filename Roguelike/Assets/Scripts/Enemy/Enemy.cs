using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _armor;
    [SerializeField] private PlayerHealth _target;

    public PlayerHealth Target => _target;

    public void TakeDamage(int damage)
    {
        _health -= damage * (100 - _armor);

        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
