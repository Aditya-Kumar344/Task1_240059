using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int attackDamage = 10;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        HealthBook enemyhealth = collision.GetComponent<HealthBook>();
        if (enemyhealth != null)
        {
            enemyhealth.Hit(attackDamage);
            Debug.Log(collision.name + "hit for" + attackDamage);
        }
    }
}
