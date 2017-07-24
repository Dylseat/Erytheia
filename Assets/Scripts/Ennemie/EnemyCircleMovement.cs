using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCircleMovement : MonoBehaviour
{
    public float radius = 10f;
    public float speed = 1f;
    public bool offsetIsCenter = true;
    public Vector3 offset;

    void Start()
    {
        if(offsetIsCenter)
        {
            offset = transform.position;
        }
    }

    void Update()
    {
        transform.position = new Vector3(
                    (radius * Mathf.Cos(Time.time * speed)) + offset.x,
                    (radius * Mathf.Sin(Time.time * speed)) + offset.y,
                    offset.z);
    }
}
