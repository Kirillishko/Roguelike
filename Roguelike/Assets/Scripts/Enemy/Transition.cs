using UnityEngine;

public abstract class Transition : MonoBehaviour
{
    [SerializeField] private State _targetState;

    public State TargetState => _targetState;
    protected PlayerHealth Target { get; private set; }
    public bool NeedTransit { get; protected set; }

    private void OnEnable()
    {
        NeedTransit = false;
    }

    private void Update()
    {
        if (NeedTransit)
            return;
        
        Check();
    }

    public void Init(PlayerHealth target)
    {
        Target = target;
    }

    protected abstract void Check();
}
