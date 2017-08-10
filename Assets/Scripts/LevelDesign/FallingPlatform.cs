using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public GameObject platform;
    public AudioClip warningFallSound;
    public Color ChangeColor;

    private float timerToFall = 0f;
    private float delayTime = 200;
    private bool hisFalling = false;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Debug.Log("Detected");

            if(hisFalling == false)
            {
                platform.GetComponent<SpriteRenderer>().color = ChangeColor;
                GetComponent<AudioSource>().PlayOneShot(warningFallSound);
                hisFalling = true;
            }

            platform.GetComponent<Rigidbody2D>().isKinematic = false;
            Destroy(platform, 3);
        }
    }
}
