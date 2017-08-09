using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UItextInfos : MonoBehaviour
{
    public static int numberSwitch;
    public Text displayText;

    void Awake()
    {
        displayText = GetComponent<Text>();
        numberSwitch = 0;
    }

    void Update()
    {
        displayText.text = "Activated Crystals : \n" + numberSwitch + " / 5";
    }

    /*void OnTriggerEnter2D(Collider2D other)
    {

        other.gameObject.SetActive(false);
        numberSwitch = numberSwitch + 1;
        SetNumberSwitchText();

        if(other.gameObject.CompareTag("Interruptor"))
        {
           
        }
    }*/
    
}
