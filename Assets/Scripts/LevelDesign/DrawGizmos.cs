using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawGizmos : MonoBehaviour
{

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 tempOffset = new Vector3(gameObject.GetComponent<BoxCollider2D>().offset.x, gameObject.GetComponent<BoxCollider2D>().offset.y, 0);
        Vector3 scale = new Vector3(gameObject.GetComponent<BoxCollider2D>().size.x, gameObject.GetComponent<BoxCollider2D>().size.y, 1);
        Gizmos.DrawWireCube((transform.position + tempOffset), scale);
    }
}
