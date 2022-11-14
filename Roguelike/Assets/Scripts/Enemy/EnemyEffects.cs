using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEffects : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;

    private MaterialPropertyBlock _materialPropertyBlock;
    private Renderer _renderer;
    private static readonly int _alphaClipThreshold = Shader.PropertyToID("_AlphaClipThreshold");

    private void OnEnable()
    {
        _enemy.Die += OnDie;
    }

    private void OnDisable()
    {
        _enemy.Die -= OnDie;
    }

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _materialPropertyBlock = new MaterialPropertyBlock();
        _renderer.GetPropertyBlock(_materialPropertyBlock);

        StartCoroutine(ChangeAlpha(1, true));
    }

    private void OnDie()
    {
        StartCoroutine(ChangeAlpha(1, false));
        StartCoroutine(Destroy(1));
    }

    private IEnumerator Destroy(float duration)
    {
        yield return new WaitForSeconds(duration);
        Destroy(_enemy.gameObject);
    }

    private IEnumerator ChangeAlpha(float duration, bool isAppearing)
    {
        float timer = 0f;

        while (timer < 1f)
        {
            timer = Mathf.Min(timer + Time.deltaTime / duration, 1f);

            float alpha;
            if (isAppearing)
                alpha = 1 - timer;
            else
                alpha = timer;

            _materialPropertyBlock.SetFloat(_alphaClipThreshold, alpha);
            _renderer.SetPropertyBlock(_materialPropertyBlock);
            yield return null;
        }
    }
}
