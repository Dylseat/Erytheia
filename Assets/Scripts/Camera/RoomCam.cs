using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class RoomCam : MonoBehaviour
{
    // Initialize variables.
    public float cameraScale = 5;

    private BoxCollider2D boxCollider2D;
    private GameObject mainCamera;
    private Camera _camera;
    private CameraLogic cameraLogic;
    private GameObject player;
    private float roomLeftBound;
    private float roomRightBound;
    private float roomLowerBound;
    private float roomUpperBound;

    // Find reference to the main camera and the player character.
    void Awake()
    {
        try
        {
            mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            _camera = mainCamera.GetComponent<Camera>();
            cameraLogic = mainCamera.GetComponent<CameraLogic>();
            boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
        }
        catch
        {
            Debug.LogError("Cannot find gameobject with tag 'MainCamera'");
            Debug.Break();
            //UnityEditor.EditorApplication.isPlaying = false;
        }

        player = GameObject.FindGameObjectWithTag("Player");

        if(player == null)
        {
            Debug.LogError("Cannot find gameobject with tag 'Player'");
            Debug.Break();
            //UnityEditor.EditorApplication.isPlaying = false;
        }
    }

    void Start()
    {
        // Find the edges of the room.
        roomLeftBound = transform.position.x + boxCollider2D.offset.x - boxCollider2D.size.x / 2;
        roomRightBound = transform.position.x + boxCollider2D.offset.x + boxCollider2D.size.x / 2;
        roomLowerBound = transform.position.y + boxCollider2D.offset.y - boxCollider2D.size.y / 2;
        roomUpperBound = transform.position.y + boxCollider2D.offset.y + boxCollider2D.size.y / 2;

        // If the player starts in this room, set the camera.
        if((player.transform.position.x > roomLeftBound) && (player.transform.position.x < roomRightBound) &&
           (player.transform.position.y > roomLowerBound) && (player.transform.position.y < roomUpperBound))
        {
            SetCameraToRoom();
        }
    }

    // Draw BoxCollider2D in the editor.
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Vector3 tempOffset = new Vector3(gameObject.GetComponent<BoxCollider2D>().offset.x, gameObject.GetComponent<BoxCollider2D>().offset.y, 0);
        Vector3 scale = new Vector3(gameObject.GetComponent<BoxCollider2D>().size.x, gameObject.GetComponent<BoxCollider2D>().size.y, 1);
        Gizmos.DrawWireCube((transform.position + tempOffset), scale);
    }

    // Register this room as the 'Next Room' if the player enters it.
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            cameraLogic.nextRoom = this.gameObject;
        }
    }

    // Switch to the 'Next Room' if the player exits this room.
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            cameraLogic.nextRoom.GetComponent<RoomCam>().SetCameraToRoom();
        }
    }

    // Set camera to view this room.
    public void SetCameraToRoom()
    {
        // camera.orthographicSize dictates zoom for orthographic cameras.
        _camera.orthographicSize = cameraScale;
        // cameraLogic.cameraScale dictates zoom for perspective cameras.
        cameraLogic.cameraScale = cameraScale;

        // Find the viewport ends, width & height of the camera for any aspect ratio.
        Vector3 cameraLowerLeftEnd = _camera.ScreenToWorldPoint(new Vector3(0, 0, cameraScale));
        Vector3 cameraUpperRightEnd = _camera.ScreenToWorldPoint(new Vector3(_camera.pixelWidth, _camera.pixelHeight, cameraScale));
        float cameraHalfWidth = (cameraUpperRightEnd.x - cameraLowerLeftEnd.x) / 2;
        float cameraHalfHeight = (cameraUpperRightEnd.y - cameraLowerLeftEnd.y) / 2;

        // Set the camera's horizontal and vertical bounds so we don't see outside the room.
        cameraLogic.maxXAndY = new Vector2((roomRightBound - cameraHalfWidth), (roomUpperBound - cameraHalfHeight));
        cameraLogic.minXAndY = new Vector2((roomLeftBound + cameraHalfWidth), (roomLowerBound + cameraHalfHeight));

        // Warn the developer if a room is too small.
        if(roomRightBound - roomLeftBound < cameraUpperRightEnd.x - cameraLowerLeftEnd.x)
        {
            Debug.LogWarning("Room [" + this.gameObject.name + "] is not wide enough for the camera scale.");
        }
        if(roomUpperBound - roomLowerBound < cameraUpperRightEnd.y - cameraLowerLeftEnd.y)
        {
            Debug.LogWarning("Room [" + this.gameObject.name + "] is not tall enough for the camera scale.");
        }
    }
}