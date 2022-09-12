using UnityEngine;

//[CreateAssetMenu(menuName = "BulletMovement/CurveBullet")]
public class CurveBullet : BulletMovement
{
    [SerializeField] private AnimationCurve _animationY;
    [SerializeField] private float _step;
    private float _currentStep = 0;


    public override void Move(Bullet bullet, Vector3 spawnPosition, Vector3 targetPosition, float speed)
    {
        targetPosition.y -= 3.5f;
        Vector3 newPosition = Vector3.Lerp(spawnPosition, targetPosition, _currentStep);
        newPosition.y += _animationY.Evaluate(_currentStep);
        bullet.transform.position = newPosition;

        _currentStep += _step;
    }
}
