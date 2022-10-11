using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(PlayerInputOld))]
public class Player : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private PlayerTracker _playerCamera;
    private PlayerMover _mover;
    private PlayerInputOld _input;

    private void Start()
    {
        _mover = GetComponent<PlayerMover>();
        _input = GetComponent<PlayerInputOld>();
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
