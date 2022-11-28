using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public Bullet b_data;
    [SerializeField, TagSelector] private string[] destroyTags = new string[] { };
    [SerializeField] private GameObject destroyParticle;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private BoxCollider2D col;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();

        sr.sprite = b_data.b_sprite;
    }

    private void Start()
    {
        rb.AddForce(transform.right * b_data.b_velocity);
        col.size = sr.size;
    }

    public int MakeDamage(int charLife)
    {
        return charLife -= b_data.b_hitDmg;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (string item in destroyTags)
        {
            if (collision.transform.CompareTag(item))
            {
                Instantiate(destroyParticle, transform.position, transform.rotation, GameObject.FindGameObjectWithTag("Destroyable").transform);
                Destroy(gameObject);
                break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (string item in destroyTags)
        {
            if (collision.transform.CompareTag(item))
            {
                Instantiate(destroyParticle, transform.position, transform.rotation, GameObject.FindGameObjectWithTag("Destroyable").transform);
                Destroy(gameObject);
                break;
            }
        }
    }
}
