/*
 * TO DO : SWITCH TO RAYCAST BASED VISION 
 *  FIX DIFFERENT LEVEL CHASING  (AN ENEMY HIGHER THAN THE PLAYER RESULTS IN THE ENEMY FLOATING TO HIS TARGET IF HE SEES HIM)
 */
using UnityEngine;
using System.Collections;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField]
    int maxHealth = 5;
    [SerializeField]
    int currentHealth = 5;
    [SerializeField]
    float timeToDie = 0.8f;
    [SerializeField]
    float velocity = 2.5f;

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

    private PlayerCharacter player;

    void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>();
    }

    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector3(velocity, 0.0f, 0.0f);

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
            Die();
        }
    }

    void OnDrawGizmos()
    {
        //Draws a line showing what makes the enemy rotate
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(sightStart.position, sightEnd.position);
    }

    public void Damage(int damage)
    {
        currentHealth -= damage;
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
        }
    }
}