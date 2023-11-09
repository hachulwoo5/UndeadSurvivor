using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public float speed = 5f;
    public float detectionRadius = 5f;

    private Rigidbody2D rb;
    private Transform target;
    private List<Transform> enemies = new List<Transform>();


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (target == null)
        {
            FindRandomEnemy();
        }
        else
        {
            Vector2 direction = target.position - transform.position;
            rb.MovePosition(rb.position + direction.normalized * speed * Time.deltaTime);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(-angle, Vector3.forward);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            enemies.Add(collision.transform);
            FindRandomEnemy();
            enemies.Remove(collision.transform);
           

        }
    }

    private void FindRandomEnemy()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
        List<Collider2D> availableColliders = new List<Collider2D>();

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemy") && !enemies.Contains(collider.transform))
            {
                availableColliders.Add(collider);
            }
        }

        if (availableColliders.Count > 0)
        {
            int randomIndex = Random.Range(0, availableColliders.Count);
            target = availableColliders[randomIndex].transform;
        }
       
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}