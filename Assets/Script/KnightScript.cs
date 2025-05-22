using UnityEngine;

public class KnightScript : MonoBehaviour
{
    public float walkSpeed = 3f;
    public float detectionRange = 3f;
    public float attackRange = 1f;
    public AudioClip enemyAttack;
    public LayerMask playerLayer;

    private SFXManager audiosfx;
    private Transform player;
    private Rigidbody2D rb;
    private Animator animator;
    private HealthBook healthScript;
    private WIn win;

    private float attackCooldown = 0.5f; // 2 attacks per second
    private float lastAttackTime = 0f;

    private bool _isMoving = false;
    public bool IsMoving
    {
        get => _isMoving;
        private set
        {
            _isMoving = value;
            animator.SetBool("IsMoving", value);
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        healthScript = GetComponent<HealthBook>();
        win = GetComponent<WIn>();
        GameObject playerObj = GameObject.FindGameObjectWithTag("Character");
        player = playerObj.transform;
        audiosfx = GameObject.FindGameObjectWithTag("Audio").GetComponent<SFXManager>();
    }

    private void FixedUpdate()
    {
        if (player == null) return;

        float horizontalDistance = Mathf.Abs(player.position.x - transform.position.x);
        float direction = Mathf.Sign(player.position.x - transform.position.x);

        Vector2 rayDirection = new Vector2(direction, 0f);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection, detectionRange, playerLayer);

        if (hit.collider != null && hit.collider.CompareTag("Character"))
        {
            FacePlayer();

            if (horizontalDistance <= attackRange)
            {
                rb.velocity = new Vector2(0f, rb.velocity.y);
                IsMoving = false;

                if (Time.time - lastAttackTime >= attackCooldown)
                {
                    lastAttackTime = Time.time;
                    animator.SetTrigger("Attack");
                    audiosfx.PlaySFX(enemyAttack);
                }
            }
            else if (horizontalDistance <= detectionRange)
            {
                rb.velocity = new Vector2(direction * walkSpeed, rb.velocity.y);
                IsMoving = true;
                animator.ResetTrigger("Attack");
            }
        }
        else
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
            IsMoving = false;
        }
    }

    private void FacePlayer()
    {
        Vector3 scale = transform.localScale;
        if ((player.position.x < transform.position.x && scale.x < 0) ||
            (player.position.x > transform.position.x && scale.x > 0))
        {
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    private void Update()
    {
        if (!healthScript.IsAlive)
        {
            if (win != null)
            {
                win.lastScene();
            }

            rb.velocity = Vector2.zero;
            rb.isKinematic = true;

            Collider2D col = GetComponent<Collider2D>();
            if (col != null) col.enabled = false;

            animator.SetBool("IsMoving", false);
            this.enabled = false;

            Destroy(gameObject, 3f);
        }
    }
}