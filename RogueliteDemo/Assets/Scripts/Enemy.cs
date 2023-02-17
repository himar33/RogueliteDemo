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
        Walk,
        Pause,
    }

    [Header("Stats")]
    [SerializeField] protected float life;
    [SerializeField] public Vector2 hitDirection;
    [SerializeField] public AudioClip playerHitClip;
    [SerializeField] public AudioClip hitClip;

    [Space]

    [Header("States Events")]
    [SerializeField] protected UnityEvent attackEvent;
    [SerializeField] protected UnityEvent deathEvent;
    [SerializeField] protected UnityEvent hurtEvent;
    [SerializeField] protected UnityEvent walkEvent;

    protected EnemyState currentState;
    protected float currentLife;

    protected bool death = false;
    protected bool hurted = false;
    protected bool attacked = false;

    protected SpriteRenderer sprite;
    protected NavMeshAgent agent;
    protected BoxCollider2D coll;
    protected Animator animator;

    protected virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        coll = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    protected virtual void Start()
    {
        SetState(EnemyState.Walk);
        currentLife = life;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    protected virtual void Update()
    {
        if (GameManager.Instance.IsPaused) return;

        switch (currentState)
        {
            case EnemyState.Attack:
                if (!attacked) attackEvent.Invoke();
                break;
            case EnemyState.Death:
                if (!death) deathEvent.Invoke();
                break;
            case EnemyState.Hurt:
                if (!hurted) hurtEvent.Invoke();
                break;
            case EnemyState.Walk:
                walkEvent.Invoke();
                break;
            default:
                break;
        }
    }

    protected void SetState(EnemyState _state)
    {
        currentState = _state;
    }

    public void Resume()
    {
        animator.enabled = true;
        agent.isStopped = false;
    }

    public void Stop()
    {
        agent.isStopped = true;
        animator.enabled = false;
    }

    protected virtual void OnDisable()
    {
        attackEvent.RemoveAllListeners();
        deathEvent.RemoveAllListeners();
        hurtEvent.RemoveAllListeners();
        walkEvent.RemoveAllListeners();
    }
}
