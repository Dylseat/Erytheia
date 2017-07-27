using UnityEngine;
using System.Collections;
using Spine;
using Spine.Unity;

public class Boss : MonoBehaviour
{
    /* Animation */
    [SerializeField]
    SkeletonAnimation animBoss;

    /* Boss */
    [SerializeField]
    int maxHealth = 5;
    [SerializeField]
    int currentHealth = 5;
    [SerializeField]
    float timeToDie = 0.8f;
    [SerializeField]
    float velocity;

    Rigidbody2D m_body;
    GameObject player;

    [SerializeField]
    Transform[] pos;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animBoss = GetComponent<SkeletonAnimation>();
        m_body = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        StartCoroutine("boss");
    }

    void Update()
    {
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            StopCoroutine("boss");
        }
    }


    IEnumerator boss()
    {
        while (true)
        {
            //FIRST ATTACK
            while (transform.position.x != pos[0].position.x)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(pos[0].position.x, transform.position.y), velocity);
              
                yield return null;
            }

            transform.localScale = new Vector2(-0.5478448f, 0.5237778f);

            yield return new WaitForSeconds(2f);

            //SECOND ATTACK
            m_body.isKinematic = true;
            while (transform.position != pos[1].position)
            {
                transform.position = Vector2.MoveTowards(transform.position, pos[1].position, velocity);
                animBoss.AnimationName = "Ult";
                animBoss.loop = false;
                yield return null;
            }

            yield return new WaitForSeconds(2f);
            GetComponent<Rigidbody2D>().isKinematic = false;

            while (transform.position != pos[3].position)
            {
                transform.position = Vector2.MoveTowards(transform.position, pos[3].position, velocity);

                yield return null;
            }
            
            yield return new WaitForSeconds(3f);
         
            //THIRD ATTACK
            Transform temp;
            if (transform.position.x > player.transform.position.x)
            {
                temp = pos[2];
            }
            else
            {
                temp = pos[0];
            }

            while (transform.position.x != temp.position.x)
            {

                transform.position = Vector2.MoveTowards(transform.position, new Vector2(temp.position.x, transform.position.y), velocity);
                animBoss.AnimationName = "Move";
                animBoss.loop = true;
                yield return null;
            }
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            currentHealth--;
            animBoss.AnimationName = "Damage";
        }
    }
}