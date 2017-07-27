using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public class EnemyFly : MonoBehaviour
{
    /* Animation */
    [SerializeField]
    SkeletonAnimation animEnemy;
    
    /* fly */
    int maxHealth = 3;
    int currentHealth;
    float timeToDie = 2f;
    Collider2D col;

    // Use this for initialization
    void Start()
    {
        col = GetComponent<Collider2D>();
        animEnemy = GetComponent<SkeletonAnimation>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
            animEnemy.AnimationName = "Flight_death";
        }
    }

    public void Die()
    {
        Destroy(this.gameObject, timeToDie);
        col.enabled = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            currentHealth--;
            animEnemy.AnimationName = "Taking_damage";
            animEnemy.loop = false;
        }
    }
}
