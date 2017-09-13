using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public LevelManager LevelManager; // KillPlayer

    // Use this for initialization
    void Start()
    {
        LevelManager = FindObjectOfType<LevelManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Player")
        {
            LevelManager.currentCheckPoint = gameObject;
            Debug.Log("Activate Checkpoint -> Position: " + transform.position);
        }
    }

}