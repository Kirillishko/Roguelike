using UnityEngine;

public abstract class BulletMovement : MonoBehaviour
{
    public enum CallType
    {
        Start,
        Update
    }

    [SerializeField] private CallType _call;

    public CallType Call => _call;

    public abstract void Move(Bullet bullet, Vector3 spawnPosition, Vector3 targetPosition, float speed);
}
