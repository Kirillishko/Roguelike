using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public class DamagePopUpCreator : MonoBehaviour
{
    private const int DefaultCapacity = 10;
    private const int MaxSize = 30;
    
    [SerializeField] private float _size;
    [SerializeField] private DamagePopUp _template;
    [SerializeField, Min(1)] private float _timeToRelease;
    
    private Camera _camera;
    private ObjectPool<DamagePopUp> _pool;

    private void Start()
    {
        _camera = Camera.main;
        
        _pool = new ObjectPool<DamagePopUp>(() =>
            {
                var popUp = Instantiate(_template, transform);
                popUp.Init(_pool, _camera!.transform, _timeToRelease, _size);
                return popUp;
            },
            popUp => { popUp.gameObject.SetActive(true); },
            popUp => { popUp.gameObject.SetActive(false); },
            popUp => { Destroy(popUp.gameObject); },
            false, DefaultCapacity, MaxSize);
    }


    // private float timer = 0f;
    // [SerializeField] private float duration;
    // private void Update()
    // {
    //     timer += Time.deltaTime;
    //     
    //     if (timer > duration)
    //     {
    //         Create(Random.Range(0, 101), transform.position);
    //         timer = 0f;
    //     }
    // }

    public void Create(int damage, Vector3 position)
    {
        var popUp = _pool.Get();
        popUp.Setup(damage, position);
    }
}
