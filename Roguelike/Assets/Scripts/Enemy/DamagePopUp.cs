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

     private Transform _camera;
     private TextMeshPro _text;
     private ObjectPool<DamagePopUp> _pool;
     private float _timeToRelease;
     private bool _isWorking;
     private bool _isReleased = true;

     private void Awake()
     {
          _text = GetComponent<TextMeshPro>();
     }

     public void Init(ObjectPool<DamagePopUp> pool, Transform camera, float timeToRelease, float size)
     {
        _pool = pool;
        _camera = camera;
        _timeToRelease = timeToRelease;
        _size = size;
     }
     
     public void Setup(int damage, Vector3 position)
     {
          _text.text = damage.ToString();
          transform.position = position + Random.onUnitSphere;
          StartCoroutine(TryRelease(_timeToRelease));
          StartCoroutine(PopUp());
     }

     private IEnumerator PopUp()
     {
         const float increasingAlphaTime = 0.3f * 4;
         const float moveTime = 0.3f * 4;
         const float waitTime = 0.1f * 4;
         const float decreasingAlphaTime = 0.2f * 4;
         const float view = increasingAlphaTime + moveTime + waitTime + decreasingAlphaTime;

         _isWorking = true;
         
         StartCoroutine(CorrectView(view));
         yield return StartCoroutine(ChangeAlpha(increasingAlphaTime, true));
         StartCoroutine(Move(moveTime, 2f));
         yield return new WaitForSecondsRealtime(waitTime);
         yield return StartCoroutine(ChangeAlpha(decreasingAlphaTime, false));
         yield return new WaitUntil(() => _isWorking);
         
         Release();
     }

     private IEnumerator CorrectView(float duration)
     {
         float timer = 0f;

         while (timer < 1f)
         {
             timer = Mathf.Min(timer + Time.deltaTime / duration, 1f);

             var position = transform.position;
             var cameraPosition = _camera.position;
             float distance = Vector3.Distance(position, cameraPosition);
             transform.localScale = Vector3.one * (distance * _size);
             transform.LookAt(2 * position - cameraPosition);

             yield return null;
         }

         _isWorking = false;
     }

     private IEnumerator ChangeAlpha(float duration, bool isIncreasing)
     {
        float timer = 0f;

        while (timer < 1f)
        {
            timer = Mathf.Min(timer + Time.deltaTime / duration, 1f);

            float alpha;
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
         float timer = 0f;
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

     private IEnumerator TryRelease(float timeToRelease)
     {
         yield return new WaitForSecondsRealtime(timeToRelease);
        
         if (_isReleased == false)
         {
             Release();
         }
     }

     private void Release()
     {
         StopAllCoroutines();
         _pool.Release(this);
         _isReleased = true;
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
}
