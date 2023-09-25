using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLookAt : MonoBehaviour
{
    [SerializeField] float sensitivity;

    float xRotation = 0.0f;
    float yRotation = 0.0f;

    private void Update()
    {
        xRotation -= Input.GetAxis("Mouse Y") * Time.deltaTime * sensitivity;
        yRotation += Input.GetAxis("Mouse X") * Time.deltaTime * sensitivity;

        //xRotation = Mathf.Clamp(xRotation, -90f, 90f);             // to stop the player from looking above/below 90

        transform.localEulerAngles = new Vector3(xRotation, yRotation, 0);
    }
}
