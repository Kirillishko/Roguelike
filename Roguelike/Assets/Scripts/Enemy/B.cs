using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log("B Awake");
    }

    private void OnEnable()
    {
        Debug.Log("B OnEnable");
    }

    private void OnDisable()
    {
        Debug.Log("B OnDisable");
    }

    private void OnDestroy()
    {
        Debug.Log("B OnDestroy");
    }

    private void Start()
    {
        Debug.Log("B Start");
    }
    
    private void Update()
    {
        Debug.Log("B Update");
    }
}
