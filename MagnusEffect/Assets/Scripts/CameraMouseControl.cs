using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMouseControl : MonoBehaviour
{
    public float sensitivity = 2.0f;     // Mouse sensitivity for camera movement
    public float maxYAngle = 80.0f;     // Maximum vertical angle for looking up and down

    private float rotationY = 0.0f;
    private float rotationX = 0.0f;
    private float rotationZ = 0.0f;

    private void Update()
    {
        // Get mouse input for camera rotation
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = -Input.GetAxis("Mouse Y");  // Invert Y-axis for natural camera movement

        // Calculate rotation along Y-axis (horizontal rotation)
        transform.Rotate(0, mouseX * sensitivity, 0);

        // Calculate rotation along X-axis (vertical rotation) with clamping
        rotationY += mouseY * sensitivity;
        rotationY = Mathf.Clamp(rotationY, -maxYAngle, maxYAngle);
        rotationY = Mathf.Clamp(rotationX, -maxYAngle, maxYAngle);
        rotationY = Mathf.Clamp(rotationZ, -maxYAngle, maxYAngle);

        // Apply the rotation to the camera
        transform.localRotation = Quaternion.Euler(rotationY, transform.localEulerAngles.y, 0);
    }
}
