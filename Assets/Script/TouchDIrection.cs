using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchDIrection : MonoBehaviour
{
    public ContactFilter2D filter;
    public float groundDistance = 0.05f;

    CapsuleCollider2D touchingCol;
    Animator animator;
    RaycastHit2D[] groundHits = new RaycastHit2D[5];
    private bool _isGrounded;

    public bool IsGrounded
    {
        get
        {
            return _isGrounded;
        }
        private set
        {
            _isGrounded = value;
            animator.SetBool("isGrounded", value);
        }
    }
    private void Awake()
    {
        touchingCol = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
    }
    void FixedUpdate()
    {
        IsGrounded = touchingCol.Cast(Vector2.down, filter, groundHits, groundDistance) > 0;
    }
}
