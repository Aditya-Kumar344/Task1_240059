using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBook : MonoBehaviour
{
    Animator animator;
    public HealthBar healthbar;
    public int _maxHealth = 100;

    public int MaxHealth
    {
        get
        {
            return _maxHealth;
        }
        set
        {
            _maxHealth = value;
        }
    }

    private bool _isAlive = true;
    public bool IsAlive
    {
        get
        {
            return _isAlive;
        }
        set
        {
            _isAlive = value;
            animator.SetBool("IsAlive", value);
        }
    }
    public int health = 100;

    public int Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
            healthbar.SetHealth(health);
            if (health <= 0)
            {
                IsAlive = false;
            }
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        healthbar.SetMaxHealth(MaxHealth);
    }
    public void Hit(int damage)
    {
        if (IsAlive)
        {
            Health -= damage;
            healthbar.SetHealth(Health);
        }
    }
}
