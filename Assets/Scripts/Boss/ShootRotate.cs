using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootRotate : MonoBehaviour
{
    [SerializeField]
    private float speedMin = 10;
    [SerializeField]
    private float speedMax = 20;


    private float speed;

    // Use this for initialization
    void Start()
    {
        speed = Random.Range(speedMin, speedMax);
        speed = Random.value < 0.5f ? speed : -speed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.back * Time.deltaTime * speed);
    }
}
