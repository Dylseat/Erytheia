using UnityEngine;
using System.Collections;
using Spine;
using Spine.Unity;

public class EnemyWolf : MonoBehaviour
{
    /* Animation */
    [SerializeField]
    SkeletonAnimation animEnemy;
    int animDamage = 0;

    /* wolf */
    [SerializeField]
    int maxHealth = 5;
    [SerializeField]
    int currentHealth = 5;
    [SerializeField]
    float timeToDie = 0.8f;
    [SerializeField]
    float velocity = 2.5f;
    [SerializeField]
    bool isMoving;
    Rigidbody2D m_Body;

    /* Audio */
    [SerializeField]
    AudioClip soundDeath;
    AudioSource m_Sound;

    /* detection wall */
    [SerializeField]
    Transform sightStart;
    [SerializeField]
    Transform sightEnd;
    [SerializeField]
    LayerMask detecting;

    [SerializeField]
    bool collidingWall;

    [SerializeField]
    float maxDistDetection;

    [SerializeField]
    Transform groundCheck;
    [SerializeField]
    LayerMask whatIsGround;
    [SerializeField]
    bool grounded = true;
    private float groundRadius = 0.15f;

    private bool facingRight = true;

    void Start()
    {
        animEnemy = GetComponent<SkeletonAnimation>();
        currentHealth = maxHealth;
        m_Body = GetComponent<Rigidbody2D>();
        m_Sound = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(isMoving)
        {
            Move();
        }
        else
        {
            if(animDamage > 0)
            {
                animEnemy.AnimationName = "degats";
                animDamage--;
            }
            else
            {
                animEnemy.AnimationName = "loup pose base";
                animEnemy.loop = true;
            }
        }

        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        if (grounded == false)
        {
            Flip();
            velocity *= -1;
        }

        //Checks if the monster walked into a wall
        collidingWall = Physics2D.Linecast(sightStart.position, sightEnd.position, detecting);

        if (collidingWall)
        {
            Flip();
            velocity *= -1;
        }

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            m_Sound.PlayOneShot(soundDeath);
            Die();
            animEnemy.AnimationName = "mort";
            velocity = 0;
            animEnemy.loop = false;
        }
    }

    void Move()
    {
        m_Body.velocity = new Vector3(velocity, m_Body.velocity.y);
        if(animDamage > 0)
        {
            animEnemy.AnimationName = "degats";
            animDamage--;
        }
        else
        {
            animEnemy.AnimationName = "marche";
            animEnemy.loop = true;
        }
    }

    void OnDrawGizmos()
    {
        //Draws a line showing what makes the enemy rotate
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(sightStart.position, sightEnd.position);
    }

    public void Die()
    {
        Destroy(this.gameObject, timeToDie);
    }

    void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        facingRight = !facingRight;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            currentHealth--;
            animDamage = 100;
        }
    }
}