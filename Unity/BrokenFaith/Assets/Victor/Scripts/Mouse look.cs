using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float Sensibilidad = 100;
    public Transform playerBody;
    public float xRotacion;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * Sensibilidad * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * Sensibilidad * Time.deltaTime;

        xRotacion -= mouseY;
        xRotacion = Mathf.Clamp(xRotacion, -90, 90);

        transform.localRotation = Quaternion.Euler(xRotacion, 0, 0);

        playerBody.Rotate(Vector3.up * mouseX);
        print(xRotacion);

    }
}