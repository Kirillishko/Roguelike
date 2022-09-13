using System.Collections;
using UnityEngine;

//[CreateAssetMenu(menuName = "BulletMovement/CurveBullet")]
public class CurveBullet : BulletMovement
{
    //[SerializeField] private AnimationCurve _animationY;
    //[SerializeField] private float _step;
    //private float _currentStep = 0;


    //public override void Move(Bullet bullet, Vector3 spawnPosition, Vector3 targetPosition, float speed)
    //{
    //    targetPosition.y -= 3.5f;
    //    Vector3 newPosition = Vector3.Lerp(spawnPosition, targetPosition, _currentStep);
    //    newPosition.y += _animationY.Evaluate(_currentStep);
    //    bullet.transform.position = newPosition;

    //    _currentStep += _step;
    //}

    [SerializeField] private float _initialVelocity;
    [SerializeField] private float _angle;
    [SerializeField] private LineRenderer _line;
    [SerializeField] private float _step;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _height;
    [SerializeField] private float _speedMultiplier;
    private Camera _camera;

    //private void Start()
    //{
    //    _camera = Camera.main;
    //}

    //private void Update()
    //{
    //    Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

    //    if (Physics.Raycast(ray, out RaycastHit hit))
    //    {
    //        //hit.point.y += 1;
    //        Vector3 point = hit.point;
    //        //point.y -= 5f;
    //        Vector3 direction = point - _firePoint.position;
    //        Vector3 groundDirection = new Vector3(direction.x, 0, direction.z);
    //        Vector3 targetPos = new Vector3(groundDirection.magnitude, direction.y, 0);

    //        float angle = _angle * Mathf.Deg2Rad;
    //        float v0;
    //        float time;
    //        targetPos.z = 0;
    //        float height = targetPos.y + targetPos.magnitude / 2f;
    //        height = Mathf.Max(0.01f, height);
    //        //CalculatePathWithHeight(targetPos, height, out angle, out v0, out time);
    //        CalculatePath(targetPos, angle, out v0, out time);

    //        DrawPath(groundDirection.normalized, v0, angle, time, _step);
    //        if (Input.GetKeyDown(KeyCode.Space))
    //        {
    //            StopAllCoroutines();
    //            StartCoroutine(Coroutine_Movement(groundDirection.normalized, v0, angle, time));
    //        }
    //    }
    //}

    private void DrawPath(Vector3 direction, float v0, float angle, float time, float step)
    {
        step = Mathf.Max(0.05f, step);
        _line.positionCount = (int)(time / step) + 2;
        int count = 0;

        for (float i = 0; i < time; i += step)
        {
            float x = v0 * i * Mathf.Cos(angle);
            float y = v0 * i * Mathf.Sin(angle) - (1f / 2f) * -Physics.gravity.y * Mathf.Pow(i, 2);
            _line.SetPosition(count, _firePoint.position + direction * x + Vector3.up * y);
            count++;
        }

        float xFinal = v0 * time * Mathf.Cos(angle);
        float yFinal = v0 * time * Mathf.Sin(angle) - (1f / 2f) * -Physics.gravity.y * Mathf.Pow(time, 2);
        _line.SetPosition(count, _firePoint.position + direction * xFinal + Vector3.up * yFinal);

    }

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

    private IEnumerator Coroutine_Movement(Rigidbody rigidbody, Vector3 direction, Vector3 spawnPosition, float v0, float angle, float time)
    {
        float t = 0;

        while (t < time)
        {
            float x = v0 * t * Mathf.Cos(angle);
            float y = v0 * t * Mathf.Sin(angle) - 0.5f * -Physics.gravity.y * Mathf.Pow(t, 2);
            rigidbody.MovePosition(spawnPosition + direction * x + Vector3.up * y);

            t += Time.deltaTime * _speedMultiplier;
            yield return null;
        }
    }

    public override void Move(Bullet bullet, Vector3 spawnPosition, Vector3 targetPosition, float speed)
    {
        Ray ray = new Ray(targetPosition, Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Debug.Log(hit.transform.name);
            Vector3 direction = hit.point - spawnPosition;
            Vector3 groundDirection = new Vector3(direction.x, 0, direction.z);
            Vector3 targetPos = new Vector3(groundDirection.magnitude, direction.y, 0);

            float angle;// = _angle * Mathf.Deg2Rad;
            float v0;
            float time;
            targetPos.z = 0;
            float height = targetPos.y + targetPos.magnitude / 2f;
            height = Mathf.Max(0.01f, height);
            CalculatePathWithHeight(targetPos, height / 2f, out angle, out v0, out time);
            //CalculatePath(targetPos, angle, out v0, out time);

            //DrawPath(groundDirection.normalized, v0, angle, time, _step);
            StopAllCoroutines();
            StartCoroutine(Coroutine_Movement(bullet.GetComponent<Rigidbody>(), groundDirection.normalized, spawnPosition, v0, angle, time));
        }
    }
}
