using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InLoadState : MonoBehaviour
{
    void Awake()
    {
        GameManager.State = GameManager.States.InLoading;
    }
}
