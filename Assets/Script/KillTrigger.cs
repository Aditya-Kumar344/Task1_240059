using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        HealthBook health = other.GetComponent<HealthBook>();
        health.Health = 0;
    }
}
