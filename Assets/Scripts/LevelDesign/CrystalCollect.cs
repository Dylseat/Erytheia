using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalCollect : MonoBehaviour
{
    [SerializeField] public AudioClip Sound;

    [SerializeField] public Wall door;
    [SerializeField] public Wall door2;
    [SerializeField] private bool isOn = false;
    [SerializeField] private int numberSwitchValue = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
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
                GameObject.FindGameObjectWithTag("TextInfos").GetComponent<PopupText>().UpdateText(numberSwitchValue);

                // Audio
                GetComponent<AudioSource>().PlayOneShot(Sound);

                // FX
                FXManager.Instance.FX(transform.position);

            }

            Destroy(gameObject);
        }
        
    }
}
