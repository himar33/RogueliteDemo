using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;
using MyBox;

public class PlayerController : MonoBehaviour
{
    public enum PlayerState
    {
        NORMAL,
        HITTED,
        DEATH
    }

    [Header("Player Stats")]
    [SerializeField] private float baseSpeed;
    [SerializeField] private float baseFireRate;
    [SerializeField] private float shootWalkSpeed;
    [SerializeField] private float hitTime;

    [Space]

    [Header("References")]
    [SerializeField] private Transform shotPoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private ParticleSystem particles;

    [ReadOnly] public float multAttack = 1f;
    [ReadOnly] public float multFireRate = 1f;
    [ReadOnly] public float multSpeed = 1f;

    private LifeBarController lifeBar;
    private PlayerState currentState;
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
    private AudioSource audioSource;

    private void Start()
    {
        currentState = PlayerState.NORMAL;
        handTransform = transform.GetChild(0);
        gunTransform = handTransform.GetChild(0);

        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        lifeBar = FindObjectOfType<LifeBarController>();

        handSR = handTransform.GetComponent<SpriteRenderer>();
        gunSR = gunTransform.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (GameManager.Instance.IsPaused) return;

        switch (currentState)
        {
            case PlayerState.NORMAL:
                NormalUpdate();
                break;
            case PlayerState.HITTED:
                StartCoroutine(Hitted());
                break;
            case PlayerState.DEATH:
                break;
            default:
                break;
        }
    }

    private void NormalUpdate()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        float currSpeed = baseSpeed;
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

        rb.velocity = currSpeed * inputMovement * multSpeed;
        anim.SetFloat("velocity", inputMovement.magnitude);
    }

    private IEnumerator Shoot()
    {
        audioSource.PlayOneShot(bulletPrefab.GetComponent<BulletController>().b_data.b_sfx);
        CameraShaker.Instance.ShakeOnce(0.25f, 4f, 0.15f, 1f);
        particles.Play();
        canShoot = false;
        GameObject bullet = Instantiate(bulletPrefab, shotPoint.position, shotPoint.rotation, GameObject.FindGameObjectWithTag("Destroyable").transform);
        bullet.GetComponent<BulletController>().SetMultAttack(multAttack);

        yield return new WaitForSeconds(baseFireRate / multFireRate);
        canShoot = true;
    }

    public IEnumerator Hitted()
    {
        anim.SetBool("hurt", true);
        CameraShaker.Instance.ShakeOnce(0.25f, 4f, 0.15f, 1f);

        yield return new WaitForSeconds(hitTime);

        anim.SetBool("hurt", false);
        currentState = PlayerState.NORMAL;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Hit"))
        {
            Vector2 hitDir = collision.GetComponentInParent<Enemy>().hitDirection;
            rb.velocity = Vector2.zero;
            lifeBar.TakeOff();

            if (collision.transform.position.x > transform.position.x) hitDir.x *= -1;

            audioSource.PlayOneShot(collision.GetComponentInParent<Enemy>().playerHitClip);
            rb.AddForce(hitDir);
            currentState = PlayerState.HITTED;
            Debug.Log("hitted");
        }
    }

    public void Resume()
    {
        anim.enabled = true;
    }

    public void Stop()
    {
        anim.enabled = false;
    }
}
