using UnityEngine;

//[RequireComponent(typeof(Enemy))]
public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] private State _firstState;
    private Player _target;

    public State CurrentState { get; private set; }

    private void Start()
    {
        _target = GetComponent<Enemy>().Target;
        Reset(_firstState);
    }

    private void Update()
    {
        if (CurrentState == null)
            return;

        var nextState = CurrentState.GetNextState();

        if (nextState != null)
            Transit(nextState);
    }

    private void Reset(State startState)
    {
        CurrentState = startState;

        if (CurrentState != null)
            CurrentState.Enter(_target);
    }

    private void Transit(State nextState)
    {
        if (CurrentState != null)
            CurrentState.Exit();

        CurrentState = nextState;

        if (CurrentState != null)
            CurrentState.Enter(_target);
    }
}
