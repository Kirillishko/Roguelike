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
     [SerializeField] private float _koef;
     
     private TextMeshPro _text;
     private ObjectPool<DamagePopUp> _pool;

     private void Awake()
     {
          _text = GetComponent<TextMeshPro>();
     }

     public void Init(ObjectPool<DamagePopUp> pool)
     {
          _pool = pool;
     }

     private Transform _camera;
     
     public void Setup(int damage, Transform camera, Vector3 position, float koef)
     {
          _text.text = damage.ToString();
          _camera = camera;
          _koef = koef;

          transform.position = position + Random.onUnitSphere;
          
          PopUp();
     }

     private async void PopUp()
     {
          transform.localScale = Vector3.one;
          CorrectView(1f);
          await ChangeAlpha(0.3f, true);
          Move(0.3f, 5f);
          await Task.Delay(100);
          await ChangeAlpha(0.2f, false);
          
          _pool.Release(this);
     }

     private async Task ChangeAlpha(float duration, bool isIncreasing, float startAlpha = 0f)
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

               await Task.Yield();
          }
     }
     
     private async Task Move(float duration, float length)
     {
          var timer = 0f;
          var startPosition = transform.position;
          var newPosition = startPosition;

          while (timer < 1f)
          {
               timer = Mathf.Min(timer + Time.deltaTime / duration, 1f);
               
               newPosition.y = startPosition.y - timer * length;
               transform.position = newPosition;

               await Task.Yield();
          }
     }

     private async void CorrectView(float duration)
     {
          float distance;
          
          var timer = 0f;
          var startScale = Vector3.one;

          while (timer < 1f)
          {
               timer = Mathf.Min(timer + Time.deltaTime / duration, 1f);

               distance = Vector3.Distance(transform.position, _camera.position);
               transform.localScale = Vector3.one * (distance * _koef);
               transform.LookAt(2 * transform.position - _camera.position);
               
               await Task.Yield();
          }
     }
     
     // private IEnumerator PopUp()
     // {
     //      yield return StartCoroutine(ChangeAlpha(0.5f, true));
     //      StartCoroutine(ChangeAlpha(0.5f, false));
     //      yield return StartCoroutine(Move(0.5f, 5f));
     //      
     //      _pool.Release(this);
     // }
     //
     // private IEnumerator ChangeAlpha(float duration, bool isIncreasing, float startAlpha = 0f)
     // {
     //      var timer = 0f;
     //      var reverser = Convert.ToInt32(isIncreasing);
     //
     //      while (timer < 1f)
     //      {
     //           timer = Mathf.Min(timer + Time.deltaTime / duration, 1f);
     //           _text.alpha = reverser - timer;
     //
     //           Debug.Log("ChangeAlpha");
     //           yield return null;
     //      }
     // }
     //
     // private IEnumerator Move(float duration, float length)
     // {
     //      var timer = 0f;
     //      var startPosition = transform.position;
     //      var newPosition = startPosition;
     //
     //      while (timer < 1f)
     //      {
     //           Debug.Log("Move");
     //           timer = Mathf.Min(timer + Time.deltaTime / duration, 1f);
     //           
     //           newPosition.y = startPosition.y - timer * length;
     //           transform.position = newPosition;
     //
     //           yield return null;
     //      }
     // }
}
