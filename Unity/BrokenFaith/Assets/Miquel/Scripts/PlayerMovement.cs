using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f;
    public float sprintMultiplier = 2.0f;
    private float yaw, pitch;
    public float SpeedH = 3, SpeedV = 3;

    public Transform cameraTransform;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Cuando queramos que haya deslizamiento, cambiamos esta variable
        // rb.drag = 0;
    }

    void Update()
    {
        float xMovement = Input.GetAxis("Horizontal");
        float zMovement = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(xMovement, 0.0f, zMovement) * speed * Time.deltaTime;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            movement *= sprintMultiplier;
        }

        transform.Translate(movement);

        yaw += SpeedH * Input.GetAxis("Mouse X");
        transform.eulerAngles = new Vector3(0f, yaw, 0f);

        if (cameraTransform != null)
        {
            pitch -= SpeedV * Input.GetAxis("Mouse Y");
            pitch = Mathf.Clamp(pitch, -90f, 90f); // Limita el pitch para evitar la rotación completa
            cameraTransform.localEulerAngles = new Vector3(pitch, 0f, 0f);
        }

    }
}
