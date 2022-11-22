using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public enum EnemyState
    {
        Attack,
        Death,
        Hurt,
        Walk
    }

    [Header("State events")]
    [SerializeField] private UnityEvent attackEvent;
    [SerializeField] private UnityEvent deathEvent;
    [SerializeField] private UnityEvent hurtEvent;
    [SerializeField] private UnityEvent walkEvent;

    private EnemyState currentState;

    private void Start()
    {
        currentState = EnemyState.Walk;
    }

    private void Update()
    {
        switch (currentState)
        {
            case EnemyState.Attack:
                attackEvent.Invoke();
                break;
            case EnemyState.Death:
                deathEvent.Invoke();
                break;
            case EnemyState.Hurt:
                hurtEvent.Invoke();
                break;
            case EnemyState.Walk:
                walkEvent.Invoke();
                break;
            default:
                break;
        }
    }

    public void SetState(EnemyState _state)
    {
        currentState = _state;
    }
}
