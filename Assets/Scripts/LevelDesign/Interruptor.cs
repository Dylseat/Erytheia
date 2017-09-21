using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interruptor : MonoBehaviour
{
    [SerializeField] public AudioClip Activate;
    [SerializeField] public Color ChangeColor;

    [SerializeField] public Wall door;
    [SerializeField] public Wall door2;
    [SerializeField] private bool isOn = false;
    [SerializeField] private int numberSwitchValue = 1;


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

            // Number interruptor
            //GameObject.FindGameObjectWithTag("TextInfos").GetComponent<PopupText>().UpdateText(numberSwitchValue);

            // Visual feedback
            gameObject.GetComponent<SpriteRenderer>().color = ChangeColor;

            // Audio
            GetComponent<AudioSource>().PlayOneShot(Activate);
        }
    }
}
