using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy1 : Enemy
{
    [SerializeField] private LayerMask detectLayer;
    [SerializeField] private float detectDistance;

    [Header("References")]
    [SerializeField] private AnimationClip hurtClip;
    [SerializeField] private AnimationClip deathClip;
    [SerializeField] private AnimationClip attackClip;

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
        LookAt();

        agent.SetDestination(direction.position);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, detectDistance, detectLayer);

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
        LookAt();
        animator.SetBool("Attack", true);
        attacked = true;

        yield return new WaitForSeconds(attackClip.length);

        animator.SetBool("Attack", false);
        attacked = false;
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

    public void LookAt()
    {
        if (direction.position.x < transform.position.x)
            sprite.flipX = true;
        else
            sprite.flipX = false;
    }

    public void SetAttackCollision(int state)
    {
        attackCollider.enabled = Convert.ToBoolean(state);
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
