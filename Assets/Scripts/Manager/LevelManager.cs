using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject currentCheckPoint;

    private PlayerCharacter player;

    // Use this for initialization
    void Start()
    {
        player = FindObjectOfType<PlayerCharacter>();
    }

    public void RespawnPlayer()
    {
        Debug.Log("Player Respawn");
        player.transform.position = currentCheckPoint.transform.position;
    }
}
