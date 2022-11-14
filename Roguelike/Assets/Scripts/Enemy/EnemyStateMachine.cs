using System;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] private State _firstState;

    private Enemy _enemy;
    private Player _target;

    public State CurrentState { get; private set; }

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }

    private void Start()
    {
        _target = _enemy.Target;
        Reset(_firstState);
    }

    private void OnEnable()
    {
        _enemy.Die += OnDie;
    }

    private void OnDisable()
    {
        _enemy.Die -= OnDie;
    }

    private void Update()
    {
        if (CurrentState == null)
            return;

        var nextState = CurrentState.GetNextState();

        if (nextState != null)
            Transit(nextState);
        
        CurrentState.Act();
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

    private void OnDie()
    {
        enabled = false;
    }
}
