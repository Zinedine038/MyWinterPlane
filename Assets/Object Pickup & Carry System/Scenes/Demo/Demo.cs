using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo : MonoBehaviour
{
    float rotationY;
    float rotationX;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        float movementFB = Input.GetAxis("Vertical") * 4 * Time.deltaTime;
        float movementLR = Input.GetAxis("Horizontal") * 4 * Time.deltaTime;
        Vector3 move = new Vector3(movementLR, 0, movementFB);
        transform.Translate(move);

        rotationX = Input.GetAxis("Mouse X") * 2;
        rotationY -= Input.GetAxis("Mouse Y") * 2;
        rotationY = Mathf.Clamp(rotationY, -90, 90);

        transform.Rotate(0, rotationX, 0);
        GetComponentInChildren<Camera>().transform.localRotation = Quaternion.Euler(rotationY, 0, 0);
    }
}
