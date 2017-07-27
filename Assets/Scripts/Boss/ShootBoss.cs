using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBoss : MonoBehaviour
{
    [SerializeField]
    public int damage = 1;
    [SerializeField]
    private int timerDestroy = 2;  // Per second
    [SerializeField]
    private int speed = 1;

    void Start()
    {
        // Destruction Timer
        Destroy(gameObject, timerDestroy);
    }

    void Update()
    {
        transform.position += Time.deltaTime * transform.up * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           
        }
    }
}
