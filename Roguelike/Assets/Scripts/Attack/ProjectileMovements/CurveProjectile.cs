using System.Collections;
using UnityEngine;

public class CurveProjectile : ProjectileMovement
{
    private float _speed;
    
    private float QuadraticEquation(float a, float b, float c, float sign)
    {
        return (-b + sign * Mathf.Sqrt(b * b - 4 * a * c)) / (2 * a);
    }

    private void CalculatePathWithHeight(Vector3 targetPos, float height, out float angle, out float v0, out float time)
    {
        float xt = targetPos.x;
        float yt = targetPos.y;
        float g = -Physics.gravity.y;

        float a = (-0.5f * g);
        float b = Mathf.Sqrt(2 * g * height);
        float c = -yt;

        float tPlus = QuadraticEquation(a, b, c, 1);
        float tMin = QuadraticEquation(a, b, c, -1);
        time = tPlus > tMin ? tPlus : tMin;

        angle = Mathf.Atan(b * time / xt);

        v0 = b / Mathf.Sin(angle);
    }

    private void CalculatePathWithVeloctiy(Vector3 targetPos, float height, out float angle, out float v0, out float time)
    {
        float xt = targetPos.x;
        float yt = targetPos.y;
        float g = -Physics.gravity.y;

        float a = (-0.5f * g);
        float b = Mathf.Sqrt(2 * g * height);
        float c = -yt;

        float tPlus = QuadraticEquation(a, b, c, 1);
        float tMin = QuadraticEquation(a, b, c, -1);
        time = tPlus > tMin ? tPlus : tMin;

        angle = Mathf.Atan(b * time / xt);

        v0 = b / Mathf.Sin(angle);
    }

    private void CalculatePath(Vector3 targetPos, float angle, out float v0, out float time)
    {
        float xt = targetPos.x;
        float yt = targetPos.y;
        float g = -Physics.gravity.y;

        float v1 = Mathf.Pow(xt, 2) * g;
        float v2 = 2 * xt * Mathf.Sin(angle) * Mathf.Cos(angle);
        float v3 = 2 * yt * Mathf.Pow(Mathf.Cos(angle), 2);
        v0 = Mathf.Sqrt(v1 / (v2 - v3));

        time = xt / (v0 * Mathf.Cos(angle));
    }

    private IEnumerator Movement(Rigidbody rigidbody, Vector3 direction, Vector3 spawnPosition, float v0, float angle, float time)
    {
        float t = 0;

        while (t < time)
        {
            float x = v0 * t * Mathf.Cos(angle);
            float y = v0 * t * Mathf.Sin(angle) - 0.5f * -Physics.gravity.y * Mathf.Pow(t, 2);
            rigidbody.MovePosition(spawnPosition + direction * x + Vector3.up * y);

            t += Time.deltaTime * _speed;
            yield return null;
        }
    }

    public override void Move(Projectile projectile, Vector3 spawnPosition, Vector3 targetPosition, float speed)
    {
        var ray = new Ray(targetPosition, Vector3.down);
        _speed = speed;

        if (Physics.Raycast(ray, out var hit))
        {
            var direction = hit.point - spawnPosition;
            var groundDirection = new Vector3(direction.x, 0, direction.z);
            var targetPos = new Vector3(groundDirection.magnitude, direction.y, 0);

            float angle;// = _angle * Mathf.Deg2Rad;
            float v0;
            float time;
            targetPos.z = 0;
            float height = targetPos.y + targetPos.magnitude / 2f;
            height = Mathf.Max(0.01f, height);
            CalculatePathWithHeight(targetPos, height / 2f, out angle, out v0, out time);
            //CalculatePath(targetPos, angle, out v0, out time);

            //DrawPath(groundDirection.normalized, v0, angle, time, _step);
            
            StartCoroutine(Movement(projectile.Rigidbody, groundDirection.normalized, spawnPosition, v0, angle, time));
        }
    }

    public override void Reset()
    {
        StopAllCoroutines();
    }
}
