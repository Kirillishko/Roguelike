using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

[RequireComponent(typeof(TextMeshPro))]
public class DamagePopUp : MonoBehaviour
{
     [SerializeField] private float _size;
     
     private TextMeshPro _text;
     private ObjectPool<DamagePopUp> _pool;

     private void Awake()
     {
          _text = GetComponent<TextMeshPro>();
     }

     public void Init(ObjectPool<DamagePopUp> pool, Transform camera, float size)
     {
        _pool = pool;
        _camera = camera;
        _size = size;
    }

     private Transform _camera;
     
     public void Setup(int damage, Vector3 position)
     {
          _text.text = damage.ToString();
          transform.position = position + Random.onUnitSphere;
          StartCoroutine(PopUp());
     }

    IEnumerator coroutine;

    private void Update()
    {
        Debug.Log(coroutine != null);

        if (coroutine != null)
            Debug.Log(coroutine.Current);
    }

    //private async void PopUp()
    //{
    //     transform.localScale = Vector3.one;
    //     CorrectView(1f);
    //     await ChangeAlpha(0.3f, true);
    //     Move(0.3f, 5f);
    //     await Task.Delay(100);
    //     await ChangeAlpha(0.2f, false);

    //     _pool.Release(this);
    //}

    //private async Task ChangeAlpha(float duration, bool isIncreasing, float startAlpha = 0f)
    //{
    //     var timer = 0f;
    //     float alpha;

    //     while (timer < 1f)
    //     {
    //          timer = Mathf.Min(timer + Time.deltaTime / duration, 1f);

    //          if (isIncreasing)
    //               alpha = timer;
    //          else
    //               alpha = 1 - timer;

    //          _text.alpha = alpha;

    //          await Task.Yield();
    //     }
    //}

    //private async Task Move(float duration, float length)
    //{
    //     var timer = 0f;
    //     var startPosition = transform.position;
    //     var newPosition = startPosition;

    //     while (timer < 1f)
    //     {
    //          timer = Mathf.Min(timer + Time.deltaTime / duration, 1f);

    //          newPosition.y = startPosition.y - timer * length;
    //          transform.position = newPosition;

    //          await Task.Yield();
    //     }
    //}

    //private async void CorrectView(float duration)
    //{
    //     float distance;

    //     var timer = 0f;
    //     var startScale = Vector3.one;

    //     while (timer < 1f)
    //     {
    //          timer = Mathf.Min(timer + Time.deltaTime / duration, 1f);

    //          distance = Vector3.Distance(transform.position, _camera.position);
    //          transform.localScale = Vector3.one * (distance * _size);
    //          transform.LookAt(2 * transform.position - _camera.position);

    //          await Task.Yield();
    //     }
    //}

    private IEnumerator PopUp()
    {
        float increasingAlphaTime = 0.3f;
        float moveTime = 0.3f;
        float waitTime = 0.1f;
        float decreasingAlphaTime = 0.2f;
        float view = increasingAlphaTime + moveTime + waitTime + decreasingAlphaTime;

        //StartCoroutine(CorrectView(view));
        coroutine = CorrectView(view);
        StartCoroutine(coroutine);

        yield return StartCoroutine(ChangeAlpha(increasingAlphaTime, true));
        StartCoroutine(Move(moveTime, 5f));
        yield return new WaitForSecondsRealtime(waitTime);
        yield return StartCoroutine(ChangeAlpha(decreasingAlphaTime, false));

        _pool.Release(this);
    }

    private IEnumerator ChangeAlpha(float duration, bool isIncreasing)
    {
        var timer = 0f;
        float alpha;

        while (timer < 1f)
        {
            timer = Mathf.Min(timer + Time.deltaTime / duration, 1f);

            if (isIncreasing)
                alpha = timer;
            else
                alpha = 1 - timer;

            _text.alpha = alpha;
            yield return null;
        }
    }

    private IEnumerator Move(float duration, float length)
    {
        var timer = 0f;
        var startPosition = transform.position;
        var newPosition = startPosition;

        while (timer < 1f)
        {
            timer = Mathf.Min(timer + Time.deltaTime / duration, 1f);

            newPosition.y = startPosition.y - timer * length;
            transform.position = newPosition;

            yield return null;
        }
    }

    private IEnumerator CorrectView(float duration)
    {
        float distance;
        float timer = 0f;

        while (timer < 1f)
        {
            timer = Mathf.Min(timer + Time.deltaTime / duration, 1f);

            distance = Vector3.Distance(transform.position, _camera.position);
            transform.localScale = Vector3.one * (distance * _size);
            transform.LookAt(2 * transform.position - _camera.position);

            yield return null;
        }
    }
}
