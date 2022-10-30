using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int _health;

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health < 0)
        {
            Destroy(gameObject);
        }
    }
}
