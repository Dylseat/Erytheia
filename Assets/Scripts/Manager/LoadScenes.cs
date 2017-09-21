using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScenes : MonoBehaviour
{
    [SerializeField] public string nameScene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            GameManager.Instance.LoadScene(nameScene);
        }
    }
}
