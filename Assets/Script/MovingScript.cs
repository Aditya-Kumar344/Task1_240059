using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingScript : MonoBehaviour
{
    public Transform Pos1, Pos2;
    public float speed;
    Vector3 targetPos;

    private void Start()
    {
        targetPos = Pos2.position;
    }
    private void Update()
    {
        if (Vector2.Distance(transform.position, Pos1.position) < 0.05f)
        {
            targetPos = Pos2.position;
        }
        if (Vector2.Distance(transform.position, Pos2.position) < 0.05f)
        {
            targetPos = Pos1.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Character"))
        {
            collision.transform.parent = this.transform;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Character"))
        {
            collision.transform.parent = null;
        }
    }
}
