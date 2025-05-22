using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchDIrection))]
public class PlayerController : MonoBehaviour
{
    TouchDIrection touchDIrection;

    SFXManager audiosfx;
    HealthBook healthScript;
    public float walkSpeed = 5f;
    public float airSpeed = 2f;
    public float jumpImpulse = 3f;
    Vector2 moveInput;
    private bool _isMoving = false;
    public bool IsMoving
    {
        get
        {
            return _isMoving;
        }
        private set
        {
            _isMoving = value;
            animator.SetBool("isMoving", value);
        }
    }

    public bool _IsFacingLeft = true;
    public bool IsFacingLeft
    {
        get
        {
            return _IsFacingLeft;
        }
        private set
        {
            if (_IsFacingLeft != value)
            {
                transform.localScale *= new Vector2(-1, 1);
            }
            _IsFacingLeft = value;
        }
    }
    Rigidbody2D rb;
    Animator animator;
    PlayerInput playerInput;

    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchDIrection = GetComponent<TouchDIrection>();
        healthScript = GetComponent<HealthBook>();
        playerInput = GetComponent<PlayerInput>();
        audiosfx = GameObject.FindGameObjectWithTag("Audio").GetComponent<SFXManager>();
        CoinManager.coinCount = 0;
    }

    private void FixedUpdate()
    {
        float currentSpeed = touchDIrection.IsGrounded ? walkSpeed : airSpeed;
        rb.velocity = new Vector2(moveInput.x * currentSpeed, rb.velocity.y);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        IsMoving = moveInput != Vector2.zero;
        FacingDirection(moveInput);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && touchDIrection.IsGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
        }

    }
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started && touchDIrection.IsGrounded)
        {
            audiosfx.PlaySFX(audiosfx.swordAttack);
            animator.SetTrigger("attack");
        }
    }
    private void FacingDirection(Vector2 moveInput)
    {
        if (moveInput.x < 0 && !IsFacingLeft)
        {
            IsFacingLeft = true;
        }
        else if (moveInput.x > 0 && IsFacingLeft)
        {
            IsFacingLeft = false;
        }
    }
    private void Update()
    {
        if (!healthScript.IsAlive)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            audiosfx.PlaySFX(audiosfx.coin);
            CoinManager.coinCount += 1;
        }
    }
}