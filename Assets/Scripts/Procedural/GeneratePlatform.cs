using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePlatform : MonoBehaviour
{


    //public GameObject platform;
    public Transform generationPoint;
    public ObjectPooler objectPool;

    public float distanceApartMaxH;
    public float distanceApartMinH;

    public float distanceApartMaxV;
    public float distanceApartMinV;


    private float platformWidth;



    void Start()
    {
        //platformWidth = platform.GetComponent<BoxCollider2D>().size.x;
    }

    void Update()
    {

        // if true create some new platforms
        if(transform.position.x < generationPoint.position.x)
        {
            float distanceApartH = Random.Range(distanceApartMinH, distanceApartMaxH);
            float distanceApartV = Random.Range(distanceApartMinV, distanceApartMaxV);

            transform.position = new Vector3(transform.position.x + platformWidth + distanceApartH,
                                             distanceApartV, transform.position.z);

            GameObject platform = objectPool.getPooledObject();
            platform.transform.position = transform.position;
            platform.transform.rotation = transform.rotation;
            platform.SetActive(true);
        }

    }
}
