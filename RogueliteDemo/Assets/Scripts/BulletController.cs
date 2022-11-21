using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private Bullet b_data;
    [SerializeField, TagSelector] private string[] destroyTags = new string[] { };

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (string item in destroyTags)
        {
            if (collision.transform.CompareTag(item))
            {
                Destroy(gameObject);
                break;
            }
        }
    }
}
