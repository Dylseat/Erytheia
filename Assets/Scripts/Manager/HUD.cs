using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{

    [SerializeField]
    Sprite[] leafSprites;
    [SerializeField]
    Image leafUI;

    private PlayerCharacter player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>();
    }

    void Update()
    {
        leafUI.sprite = leafSprites[player.currentHealth]; // Changes the hearts sprite according to the player's health
    }
}