using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controlador : MonoBehaviour
{
    CharacterController character;

    public float Walk = 6.0f;
    public float Jump = 10f;
    public float Gravity = 15f;
    public float Run = 25f;

    public Camera camara;
    public float Horizontal = 2.0f;
    public float Vertical = 2.0f;
    public float Rotacionmaxima = 40.0f;
    public float Rotacionminima = 50.0f;
    float h_mou, y_mou;


    private Vector3 movimiento = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        y_mou = Vertical * Input.GetAxis("Mouse Y");
        h_mou = Horizontal * Input.GetAxis("Mouse X");

        y_mou = Mathf.Clamp(y_mou, Rotacionminima, Rotacionmaxima);
        camara.transform.localEulerAngles = new Vector3(-y_mou, 0, 0);

        if (character.isGrounded)
        {
            movimiento = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            if (Input.GetKey(KeyCode.LeftShift))
            {
                movimiento = transform.TransformDirection(movimiento) * Run;
            }
            else
            {
                movimiento = transform.TransformDirection(movimiento) * Walk;
            }

            if(Input.GetKey(KeyCode.Space))
            {
                movimiento.y = Jump;
            }
        }

        movimiento.y -= Gravity * Time.deltaTime;
        character.Move(movimiento * Time.deltaTime);
    }
}
