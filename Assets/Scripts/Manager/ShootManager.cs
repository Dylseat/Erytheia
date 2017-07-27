using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootManager : MonoBehaviour
{
    [SerializeField]
    Vector2 speed;
    [SerializeField]
    float delay;
    [SerializeField]
    LayerMask whatIsEnemy;
    [SerializeField]
    bool collidingEnemy;
    private int enemyHit = 0;
    Rigidbody2D rb;
    Collider2D c2;
    Collider2D c1;
    int shootDamage = 1;
    private EnemyPatrol enemy;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        c2 = GetComponent<Collider2D>();
        rb.velocity = speed;
        Destroy(gameObject, delay);
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyPatrol>();
    }

    void Update()
    {
        rb.velocity = speed;

        if (collidingEnemy)
        {
            Debug.Log(enemyHit);
            if (enemyHit == 0)
            {
                enemyHit = 1;
                enemy.Damage(shootDamage);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Physics2D.IgnoreCollision(c2, collision.collider);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }

        if(collision.gameObject.CompareTag("Interruptor"))
        {
            Destroy(gameObject);
        }

        if(collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
