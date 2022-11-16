using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform shotPoint;
    [SerializeField] private GameObject bulletPrefab;

    private Vector2 inputMovement;
    private Rigidbody2D rb;

    private Transform handTransform;
    private Transform gunTransform;
    private Vector2 mousePosition;

    private SpriteRenderer sr;
    private SpriteRenderer handSR;
    private SpriteRenderer gunSR;

    private void Start()
    {
        handTransform = transform.GetChild(0);
        gunTransform = handTransform.GetChild(0);

        rb = GetComponent<Rigidbody2D>();

        sr = GetComponent<SpriteRenderer>();
        handSR = handTransform.GetComponent<SpriteRenderer>();
        gunSR = gunTransform.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        inputMovement.x = Input.GetAxisRaw("Horizontal");
        inputMovement.y = Input.GetAxisRaw("Vertical");

        if (mousePosition.x > handTransform.position.x && sr.flipX == true || mousePosition.x < handTransform.position.x && sr.flipX != true)
        {
            sr.flipX = !sr.flipX;
            handSR.flipX = !handSR.flipX;
            gunSR.flipY = !gunSR.flipY;
        }

        Vector2 aimDirection = mousePosition - (Vector2)handTransform.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        handTransform.eulerAngles = new Vector3(0, 0, aimAngle);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Instantiate(bulletPrefab, shotPoint);
        }

        rb.velocity = speed * Time.deltaTime * inputMovement;
    }
}
