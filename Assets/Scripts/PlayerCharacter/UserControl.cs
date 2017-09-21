using UnityEngine;
using System.Collections;
using InControl;

public class UserControl : MonoBehaviour
{
    //Using InControll for controllers. Check the link for more infos
    //http://www.gallantgames.com/

    PlayerCharacter pChar;
    // Use this for initialization
    void Awake()
    {//There are no Update before Awake
        pChar = GetComponent<PlayerCharacter>();
    }

    void Start()
    {//There can be Update before Start

    }

    /// <summary>
    /// Used for managed controller
    /// </summary>
    void manageController()
    {
        // Keyboard
        if(Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1)
        {
            pChar.move(Input.GetAxis("Horizontal"));
        }
        if(Input.GetButtonDown("Shoot"))
        {
            pChar.shoot();
        }
        if(Input.GetButton("Jump"))
        {
            pChar.jump();
        }

        if (InputManager.Devices[0].LeftStickX)
        {
            pChar.move(InputManager.Devices[0].LeftStickX);
        }

        if (InputManager.Devices[0].Action1.WasPressed)
        {
            pChar.jump();
            //Mathf.Abs(InputManager.Devices[0].LeftStickX)
        }

        if (InputManager.Devices[0].Action3.WasPressed)
        {
            pChar.shoot();
        }
    }

    // Update is called once per frame
    void Update()
    {
        manageController();
    }
}