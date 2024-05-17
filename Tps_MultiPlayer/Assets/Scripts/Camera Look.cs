
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    public RectTransform uiPanel; // Reference to your UI panel

    private float xRotation = 0f;

    void Start()
    {
      //  Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Input.GetMouseButton(0)) // Left mouse button for dragging
        {
            // Get the mouse position in screen coordinates
            Vector2 mousePosition = Input.mousePosition;

            // Check if the mouse position is within the UI panel
            if (RectTransformUtility.RectangleContainsScreenPoint(uiPanel, mousePosition))
            {
                float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
                float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

                xRotation -= mouseY;
                xRotation = Mathf.Clamp(xRotation, -90f, 90f);

                transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
                playerBody.Rotate(Vector3.up * mouseX);
            }
        }
    }
}
