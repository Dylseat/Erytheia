using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject currentCheckPoint;

    private PlayerCharacter player;

    void Awake()
    {
        GameManager.State = GameManager.States.InGame;
    }

    // Use this for initialization
    void Start()
    {
        player = FindObjectOfType<PlayerCharacter>();
        Debug.Log(GameManager.State);
    }

    public void RespawnPlayer()
    {
        Debug.Log("Player Respawn");
        player.transform.position = currentCheckPoint.transform.position;
    }
}
