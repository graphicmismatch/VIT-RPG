using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform target;
    public float borderSize = 0.1f; // Border size as a fraction of the viewport width/height
    public float smoothing = 0.1f; // Smooth the camera movement

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Get the player's position in viewport coordinates (0 to 1)
        Vector3 viewPos = mainCamera.WorldToViewportPoint(target.position);

        // Determine if the player is within the border zone
        Vector3 move = Vector3.zero;

        if (viewPos.x < borderSize)
            move.x = viewPos.x - borderSize;
        else if (viewPos.x > 1 - borderSize)
            move.x = viewPos.x - (1 - borderSize);

        if (viewPos.y < borderSize)
            move.y = viewPos.y - borderSize;
        else if (viewPos.y > 1 - borderSize)
            move.y = viewPos.y - (1 - borderSize);

        // Move the camera
        Vector3 targetPosition = transform.position + move;
        transform.position += move;
    }
}