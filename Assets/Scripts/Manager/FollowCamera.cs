/* Filipe */

using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour
{

    private Vector3 offset;
    float MinPosX = -60.01f;
    float MaxPosX = 459.2f;
    float MinPosY = -7.0f;
    float MaxPosy = -7.0f;

    [SerializeField]
    GameObject Player;

    void Awake()
    {
        Player = GameObject.Find("Player");
    }

    void Start()
    {
        offset = transform.position - Player.transform.position;
    }

    void Update()
    {

    }

    void LateUpdate()
    {
        Vector3 CameraPosition = Player.transform.position + offset;
        if (CameraPosition.x < MinPosX)
        {
            CameraPosition.x = MinPosX;
        }
        if (CameraPosition.x > MaxPosX)
        {
            CameraPosition.x = MaxPosX;
        }
        if (CameraPosition.y < MinPosY)
        {
            CameraPosition.y = MinPosY;
        }
        if (CameraPosition.y > MaxPosy)
        {
            CameraPosition.y = MaxPosy;
        }
        transform.position = CameraPosition;
    }
}
