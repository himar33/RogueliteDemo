using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : Enemy
{
    [SerializeField] private Vector3 heightPos;
    [SerializeField] float attackTime;
    [SerializeField] float detectDistance;
    [SerializeField] LayerMask detectLayer;

    [Space]

    [Header("References")]
    [SerializeField] private AnimationClip hurtClip;
    [SerializeField] private AnimationClip deathClip;

    private BoxCollider2D attackCollider;
    private Transform direction;

    protected override void Awake()
    {
        base.Awake();
        attackCollider = transform.GetChild(0).GetComponent<BoxCollider2D>();
    }

    protected override void Start()
    {
        base.Start();
        direction = GameObject.FindGameObjectWithTag("Player").transform;
    }

    protected override void Update()
    {
        base.Update();
    }

    public void WalkTo()
    {
        agent.SetDestination(direction.position + heightPos);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, detectDistance, detectLayer);

        if (hit.collider && hit.transform.CompareTag("Player"))
        {
            SetState(EnemyState.Attack);
        }
    }

    public void AttackCall()
    {
        StartCoroutine(Attack());
    }

    public IEnumerator Attack()
    {
        agent.isStopped = true;
        attacked = true;
        animator.SetBool("Attack", true);
        attackCollider.enabled = true;

        yield return new WaitForSeconds(attackTime);

        attackCollider.enabled = false;
        SetState(EnemyState.Walk);
        animator.SetBool("Attack", false);
        attacked = false;
        agent.isStopped = false;
    }

    public void HurtCall()
    {
        StartCoroutine(Hurt());
    }

    public IEnumerator Hurt()
    {
        hurted = true;
        agent.isStopped = true;

        yield return new WaitForSeconds(hurtClip.length);

        hurted = false;
        agent.isStopped = false;
        if (currentLife <= 0) SetState(EnemyState.Death);
        else SetState(EnemyState.Walk);
    }

    public void DeathCall()
    {
        StartCoroutine(Death());
    }

    public IEnumerator Death()
    {
        death = true;
        animator.SetTrigger("Death");

        coll.enabled = false;
        agent.enabled = false;

        yield return new WaitForSeconds(deathClip.length);

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Bullet"))
        {
            currentLife = collision.GetComponent<BulletController>().MakeDamage(currentLife);
            animator.SetTrigger("Hurt");
            SetState(EnemyState.Hurt);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(transform.position, transform.position - new Vector3(0, detectDistance, 0));
    }
}
