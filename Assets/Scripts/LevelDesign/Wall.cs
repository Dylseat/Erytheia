using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public int NumberForOpen = 1;

    public void Open()
    {
        NumberForOpen--;
        if(NumberForOpen <= 0)
        {
            //open door
            Destroy(gameObject);
        }
    }
}
