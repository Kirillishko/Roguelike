using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    [SerializeField] private float _speed;

    private void Update()
    {
        Move(_speed * Time.deltaTime);
    }

    private void Move(float speed)
    {
        transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, speed);
    }
}
