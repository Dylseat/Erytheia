using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interruptor : MonoBehaviour
{
    public AudioClip Activate;
    public Color ChangeColor;

    public Wall door;
    public Wall door2;
    private bool isOn = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!isOn)
        {
            if(door)
            {
                door.Open();
            }
            if(door2)
            {
                door2.Open();
            }
            isOn = true;

            gameObject.GetComponent<SpriteRenderer>().color = ChangeColor;
            GetComponent<AudioSource>().PlayOneShot(Activate);

            Destroy(this);
        }
    }
}
