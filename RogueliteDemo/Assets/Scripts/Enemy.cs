using System;
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
    [SerializeField] private Transform playerPos;

    private SpriteRenderer sr;
    private EnemyState currentState;
    private NavMeshAgent agent;
    private BoxCollider2D attackCollision;
    private Animator animator;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        attackCollision = transform.GetChild(0).GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        SetState(EnemyState.Walk);

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
        if (playerPos.position.x < transform.position.x)
            sr.flipX = true;
        else
            sr.flipX = false;

        agent.SetDestination(direction.position);
    }

    public void SetState(EnemyState _state)
    {
        currentState = _state;
    }

    public void SetAttackCollision(int state)
    {
        attackCollision.enabled = Convert.ToBoolean(state);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            SetState(EnemyState.Attack);
            animator.SetBool("Attack", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            SetState(EnemyState.Walk);
            animator.SetBool("Attack", false);
        }
    }
}
