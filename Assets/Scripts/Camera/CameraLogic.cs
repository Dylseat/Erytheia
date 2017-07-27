using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class CameraLogic : MonoBehaviour
{
    // Initialize variables.
    public float xSpeed = 4.0f;
    public float ySpeedDown = 10.0f;
    public float ySpeedUp = 1.5f;
    public float upperBound = 2.5f;

    [HideInInspector]
    public Vector2 maxXAndY;
    [HideInInspector]
    public Vector2 minXAndY;
    [HideInInspector]
    public float cameraScale;
    [HideInInspector]
    public GameObject nextRoom;

    private bool followUpwards = true;
    private GameObject player;
    private Vector3 cameraTarget;

    // Find reference to the player character.
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if(player == null)
        {
            Debug.LogError("Cannot find gameobject with tag 'Player'");
            Debug.Break();
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }

    void FixedUpdate()
    {
        cameraTarget = player.transform.position;

        float targetX = transform.position.x;
        float targetY = transform.position.y;
        float targetZ = -cameraScale;

        // Slowly move horizontally to center on the player.
        targetX = Mathf.Lerp(transform.position.x, cameraTarget.x, xSpeed * Time.deltaTime);

        // If the player is below center-screen, follow the player veritcally.
        if((cameraTarget.y - transform.position.y) < 0)
        {
            targetY = Mathf.Lerp(transform.position.y, cameraTarget.y, ySpeedDown * Time.deltaTime);
        }

        // Only follow the player upwards if the player is a set amount above center-screen.
        if((cameraTarget.y - transform.position.y) > upperBound)
        {
            followUpwards = true;
        }

        // Follow the player upwards until we are very close.
        if(followUpwards)
        {
            targetY = Mathf.Lerp(transform.position.y, cameraTarget.y, ySpeedUp * Time.deltaTime);

            if(Mathf.Abs(transform.position.y - cameraTarget.y) < 1)
            {
                followUpwards = false;
            }
        }

        // Constrain camera to room size.
        targetX = Mathf.Clamp(targetX, minXAndY.x, maxXAndY.x);
        targetY = Mathf.Clamp(targetY, minXAndY.y, maxXAndY.y);

        // Actually move the camera.
        transform.position = new Vector3(targetX, targetY, targetZ);
    }
}