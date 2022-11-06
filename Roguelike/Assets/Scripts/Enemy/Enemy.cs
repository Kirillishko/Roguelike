using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _armor;
    [SerializeField] private Player _target;

    public Player Target => _target;

    public void TakeDamage(int damage)
    {
        _health -= damage * (100 - _armor);

        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
