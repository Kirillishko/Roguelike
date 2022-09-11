using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(PlayerInput))]
public class Player : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private PlayerTracker _playerCamera;
    private PlayerMover _mover;
    private PlayerInput _input;

    private void Start()
    {
        _mover = GetComponent<PlayerMover>();
        _input = GetComponent<PlayerInput>();
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health < 0)
        {
            Destroy(gameObject);
        }
    }
}
