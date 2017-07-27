using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikeBoss : MonoBehaviour
{
    [SerializeField]
    int damage = 2;
    [SerializeField]
    float up;
    [SerializeField]
    float down;

    Rigidbody2D m_body;

    // Use this for initialization
    void Start()
    {
        m_body = GetComponent<Rigidbody2D>();
        m_body.velocity = new Vector2(m_body.velocity.x, up);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D Collider)
    {
        if (Collider.gameObject.tag == "Limit")
        { 
            m_body.velocity = new Vector2(m_body.velocity.x, down);
        }
    }
}

