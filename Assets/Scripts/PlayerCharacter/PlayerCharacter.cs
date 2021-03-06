﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using InControl;
using Spine;
using Spine.Unity;

public class PlayerCharacter : MonoBehaviour
{
    /* Animation*/
    [SerializeField]
    SkeletonAnimation animPlayer;
    int animDamage;

    /* Player */
    [SerializeField]
    float speed;
    [SerializeField]
    float jumpForce;
    [SerializeField]
    Transform groundCheck;
    [SerializeField]
    LayerMask Ground;
    [SerializeField]
    bool IsTurnedRight = true;
    [SerializeField]
    int maxHealth = 10;

    Rigidbody2D m_Body;
    bool m_Ground = false;
    float rayonGround = 0.15f;
    bool isTurnedRight = true;
    const float walkDeadZone = 0.3f;
    public int currentHealth;

    /* Audio */
    [SerializeField]
    AudioClip soundShoot;
    AudioSource m_Sound;

    /* Shoot */
    [SerializeField]
    GameObject leftBullet;
    [SerializeField]
    GameObject rightBullet;
    [SerializeField]
    Transform FirePosition;

    public LevelManager LevelManager; // KillPlayer
    [SerializeField]
    float timerToRespawn = 1f; // After his Death

    bool animationDeath = false;

    int ammoLeft = 3;

    // Use this for initialization
    void Start()
    {
        LevelManager = FindObjectOfType<LevelManager>();

        animPlayer = GetComponent<SkeletonAnimation>();
        m_Body = GetComponent<Rigidbody2D>();
        m_Sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        if (currentHealth <= 0 && !animationDeath)
        {
            currentHealth = 0;
            PlayerDead();
            animPlayer.AnimationName = "death";
        }
        else if(animationDeath)
        {
            //m_Body.velocity = new Vector2(m_Body.velocity.x, 4);
        }
    }

    void FixedUpdate()
    {
        m_Ground = Physics2D.OverlapCircle(groundCheck.position, rayonGround, Ground);
    }

    public void jump(float horizontal)
    {
        if (m_Ground)
        {
            m_Body.velocity = new Vector2(m_Body.velocity.x, jumpForce);
        }
    }

    public void move(float horizontal)
    {
        m_Body.velocity = new Vector3(speed * horizontal, m_Body.velocity.y);

        if (Mathf.Abs(horizontal) <= 0.5)
        {
            animPlayer.loop = true;
            animPlayer.AnimationName = "idle";
        }
        else
        {
            if(animDamage > 0)
            {
                animPlayer.AnimationName = "damage";
                animDamage--;
            }
            else
            {
                animPlayer.AnimationName = "move";
                animPlayer.loop = true;
            }
        }

        if (groundCheck)
        {     
            if (horizontal > 0 && !IsTurnedRight)
            {
                Flip();
            }
            else if (horizontal < 0 && IsTurnedRight)
            {
                Flip();
            }
        }
    }

    void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        IsTurnedRight = !IsTurnedRight;
    }

    public void shoot()
    {
        if (ammoLeft > 0)
        {
            animPlayer.AnimationName = "attack";
            if (IsTurnedRight)
            {
                Instantiate(rightBullet, FirePosition.position, Quaternion.identity);
                m_Sound.PlayOneShot(soundShoot);
            }

            if (!IsTurnedRight)
            {
                Instantiate(leftBullet, FirePosition.position, Quaternion.identity);
                m_Sound.PlayOneShot(soundShoot);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            currentHealth = currentHealth - 2;
            animDamage = 50;
        }

        if (collision.gameObject.tag == "DeadZone")
        {
            currentHealth = 0;
        }

        if (collision.gameObject.tag == "EnemyFly")
        {
            animDamage = 50;
            currentHealth --;
        }

        if (collision.gameObject.tag == "ShootBoss")
        {
            currentHealth --;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "ShootBoss")
        {
            animDamage = 50;
            currentHealth--;
        }

        if(collision.gameObject.tag == "DeadZone")
        {
            currentHealth = 0;
        }

    }

    public void Damage(int damage)
    {
        currentHealth -= damage;
    }

    void PlayerDead()
    {
        animationDeath = true;
        StartCoroutine(waitSeconds());

    }

    IEnumerator waitSeconds()
    {
        yield return new WaitForSeconds(timerToRespawn);
        Debug.Log("BOOM! We just waited " + timerToRespawn + " Whole SECONDS MANNN !");
        LevelManager.RespawnPlayer();
        currentHealth = maxHealth;
        Debug.Log("CurrentHealth : " + currentHealth);
        animationDeath = false;
    }
}