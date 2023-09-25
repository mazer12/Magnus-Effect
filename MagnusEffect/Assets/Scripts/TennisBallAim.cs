using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TennisBallAim : MonoBehaviour
{
    public Transform ballTransform;   // Reference to the tennis ball's Transform
    public float aimSensitivity = 2.0f;

    private float horizontalAngle = 0.0f;
    private float verticalAngle = 0.0f;

    private void Update()
    {
        // Get mouse input for aiming
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Calculate rotation around Y-axis (horizontal angle)
        horizontalAngle += mouseX * aimSensitivity;
        ballTransform.localRotation = Quaternion.Euler(verticalAngle, horizontalAngle, 0);

        // Calculate rotation around X-axis (vertical angle) with clamping
        verticalAngle -= mouseY * aimSensitivity;  // Invert vertical angle due to different axis orientation
        verticalAngle = Mathf.Clamp(verticalAngle, -90.0f, 90.0f);

        // Apply vertical rotation to the ball's local rotation
        ballTransform.localRotation *= Quaternion.Euler(0, 0, -verticalAngle);  // Invert z-rotation

        Debug.Log(GetHorizontalAngle() + " " + GetVerticalAngle());
    }

    public float GetHorizontalAngle()
    {
        return horizontalAngle;
    }

    public float GetVerticalAngle()
    {
        return verticalAngle;
    }
}
