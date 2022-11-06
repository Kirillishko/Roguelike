using UnityEngine;
using Random = UnityEngine.Random;

public class DamagePopUpCreator : MonoBehaviour
{
    [SerializeField] private float _koef;
    [SerializeField] private DamagePopUp _template;
    [SerializeField, Min(1)] private float _timeToRelease;
    private Camera _camera;
    
    private UnityEngine.Pool.ObjectPool<DamagePopUp> _pool;
    private const int _defaultCapacity = 10;
    private const int _maxSize = 30;

    private void Start()
    {
        _camera = Camera.main;;
        
        _pool = new UnityEngine.Pool.ObjectPool<DamagePopUp>(() =>
            {
                var popUp = Instantiate(_template, transform);
                popUp.Init(_pool);
                return popUp;
            },
            popUp => { popUp.gameObject.SetActive(true); },
            popUp => { popUp.gameObject.SetActive(false); },
            popUp => { Destroy(popUp.gameObject); },
            false, _defaultCapacity, _maxSize);
    }


    private float timer = 0f;
    [SerializeField] private float duration;
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > duration)
        {
            Create(Random.Range(0, 101));
            timer = 0f;
        }
    }

    private void Create(int damage)
    {
        var popUp = _pool.Get();
        popUp.Setup(damage, _camera.transform, transform.position, _koef);
    }
}
