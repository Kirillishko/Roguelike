using UnityEngine;

public abstract class ProjectileMovement : MonoBehaviour
{
    public enum CallType
    {
        Start,
        Update
    }

    [SerializeField] private CallType _call;

    public CallType Call => _call;

    public abstract void Move(Projectile projectile, Vector3 spawnPosition, Vector3 targetPosition, float speed);
}
