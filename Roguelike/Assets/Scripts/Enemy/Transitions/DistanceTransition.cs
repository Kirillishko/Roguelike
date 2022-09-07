using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceTransition : Transition
{
    [SerializeField] private float _transitionRange;

    private void Update()
    {
        
    }

    private void Check()
    {
        if (NeedTransit)
            return;

        if (Vector3.Distance(transform.position, Target.transform.position) < _transitionRange)
        {
            NeedTransit = true;
        }
    }
}
