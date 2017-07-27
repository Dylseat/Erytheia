using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnemy : MonoBehaviour
{
    [SerializeField]
    GameObject enemy;
    [SerializeField]
    Transform enemyPos;

    Collider2D m_col;

    // Use this for initialization
    void Start()
    {
        m_col = GetComponent<Collider2D>();
        
    }

    void OnTriggerEnter2D(Collider2D Collider)
    {
        if (Collider.gameObject.tag == "Player")
        {
            EnemySpawner();
        }
    }

    void EnemySpawner()
    {
        Instantiate(enemy, enemyPos.position, enemyPos.rotation);
        m_col.enabled = false;
    }
}
