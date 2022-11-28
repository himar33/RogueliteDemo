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

    [Header("Stats")]
    [SerializeField] private int lifeHits;

    [Space]

    [Header("References")]
    [SerializeField] private Transform direction;
    [SerializeField] private AnimationClip hurtClip;

    private int currentLife;
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

        currentLife = lifeHits;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void Update()
    {
        switch (currentState)
        {
            case EnemyState.Attack:
                LookAt(direction.position);
                break;
            case EnemyState.Death:
                break;
            case EnemyState.Hurt:
                StartCoroutine(Hurt());
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
        LookAt(direction.position);

        agent.SetDestination(direction.position);
    }

    public IEnumerator Hurt()
    {
        agent.isStopped = true;

        yield return new WaitForSeconds(hurtClip.length);

        agent.isStopped = false;
        if (currentLife <= 0) SetState(EnemyState.Death);
        else SetState(EnemyState.Walk);
    }

    private void LookAt(Vector3 position)
    {
        if (position.x < transform.position.x)
            sr.flipX = true;
        else
            sr.flipX = false;
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
        if (collision.transform.CompareTag("Bullet"))
        {
            currentLife = collision.GetComponent<BulletController>().MakeDamage(currentLife);
            animator.SetTrigger("Hurt");
            SetState(EnemyState.Hurt);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            if (attackCollision.enabled) attackCollision.enabled = false;
            SetState(EnemyState.Walk);
            animator.SetBool("Attack", false);
        }
    }
}
