using UnityEngine;

public abstract class ProjectileMovement : MonoBehaviour
{
    public abstract void Move(Projectile projectile, Vector3 spawnPosition, Vector3 targetPosition, float speed);
    public abstract void Reset();
}
