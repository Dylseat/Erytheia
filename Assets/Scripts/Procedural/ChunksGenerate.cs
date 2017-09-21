using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunksGenerate : MonoBehaviour
{
    [SerializeField] GameObject[] chunks = new GameObject[10];
    GameObject lastChunk;
    GameObject nextChunk;

    int chunkslength = 10;

    // Use this for initialization
    void Start ()
    {
        for(int i= 0; i != chunkslength; i++)
        {
            Random.Range(0, chunks.Length);

            Instantiate(nextChunk);
            lastChunk = nextChunk;
        }
	}
}
