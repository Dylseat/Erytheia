using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterruptorWrong : MonoBehaviour
{
    public AudioClip Activate;
    public Color ChangeColor;

    public Wall door;
    public Wall door2;
    private bool isOn = false;
    private int numberSwitchValue = 1;


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

            // Visual feedback
            gameObject.GetComponent<SpriteRenderer>().color = ChangeColor;

            // Audio
            GetComponent<AudioSource>().PlayOneShot(Activate);

            Destroy(this);
        }
    }
}
