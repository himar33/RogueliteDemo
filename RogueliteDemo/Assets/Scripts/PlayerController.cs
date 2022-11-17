using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class PlayerController : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] private float speed;
    [SerializeField] private float shootWalkSpeed;
    [SerializeField] private float fireRate;

    [Space]

    [Header("References")]
    [SerializeField] private Transform shotPoint;
    [SerializeField] private GameObject bulletPrefab;

    private bool canShoot = true;
    private Vector2 inputMovement;
    private Rigidbody2D rb;
    private Animator anim;
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
        anim = GetComponent<Animator>();

        handSR = handTransform.GetComponent<SpriteRenderer>();
        gunSR = gunTransform.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        float currSpeed = speed;
        inputMovement = new Vector2(moveX, moveY).normalized;

        Vector2 aimDirection = mousePosition - (Vector2)handTransform.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        handTransform.eulerAngles = new Vector3(0, 0, aimAngle);

        if (mousePosition.x > handTransform.position.x && sr.flipX == true || mousePosition.x < handTransform.position.x && sr.flipX != true)
        {
            sr.flipX = !sr.flipX;
            handSR.flipX = !handSR.flipX;
            gunSR.flipY = !gunSR.flipY;
        }

        if (mousePosition.x > handTransform.position.x && moveX < 0 || mousePosition.x < handTransform.position.x && moveX > 0)
        {
            anim.SetBool("runningBack", true);
        }
        else
        {
            anim.SetBool("runningBack", false);
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            currSpeed = shootWalkSpeed;
            anim.speed = 0.5f;
            if (canShoot)
            {
                StartCoroutine(Shoot());
            }
        }
        else
        {
            anim.speed = 1.0f;
        }

        rb.velocity = currSpeed * inputMovement;
        anim.SetFloat("velocity", inputMovement.magnitude);
    }

    private IEnumerator Shoot()
    {
        CameraShaker.Instance.ShakeOnce(2f, 2f, 0.15f, 1f);
        canShoot = false;
        Instantiate(bulletPrefab, shotPoint.position, shotPoint.rotation, GameObject.FindGameObjectWithTag("Destroyable").transform);
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }
}
