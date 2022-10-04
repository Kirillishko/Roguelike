using UnityEngine;

public class CloseDistanceTransition : Transition
{
    [SerializeField] private float _transitionRange;

    protected override void Check()
    {
        if (Vector3.Distance(transform.position, Target.transform.position) < _transitionRange)
        {
            NeedTransit = true;
        }
    }
}
