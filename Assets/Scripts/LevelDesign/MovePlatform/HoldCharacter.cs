using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldCharacter : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.transform.parent = gameObject.transform;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.transform.parent = null;
    }
}
