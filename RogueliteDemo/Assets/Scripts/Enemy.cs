using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
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

    [Header("References")]
    [SerializeField] private Transform direction;

    private EnemyState currentState;
    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        currentState = EnemyState.Walk;

        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void Update()
    {
        switch (currentState)
        {
            case EnemyState.Attack:
                break;
            case EnemyState.Death:
                break;
            case EnemyState.Hurt:
                break;
            case EnemyState.Walk:
                WalkTo();
                break;
            default:
                break;
        }
    }

    public void WalkTo()
    {
        agent.SetDestination(direction.position);
    }

    public void SetState(EnemyState _state)
    {
        currentState = _state;
    }
}
