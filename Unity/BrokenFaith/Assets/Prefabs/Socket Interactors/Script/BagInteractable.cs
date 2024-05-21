using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BagInteractable : MonoBehaviour
{
    public GameObject cubo;
    public InputActionProperty triggerAction;
    public GameObject panelInventario;
    private bool estaDentro;

    private void Start()
    {
        estaDentro = false;
    }

    private void Update()
    {
        transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y - .5f, Camera.main.transform.position.z - .6f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "RightHand Controller")
        {
            Debug.Log("Dentro de cubo");
            estaDentro = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "RightHand Controller")
        {
            estaDentro = false;
        }
    }

    private void OnEnable()
    {
        triggerAction.action.performed += OnTriggerPressed;
        triggerAction.action.Enable();
    }

    private void OnDisable()
    {
        triggerAction.action.performed -= OnTriggerPressed;
        triggerAction.action.Disable();
    }

    private void OnTriggerPressed(InputAction.CallbackContext context)
    {
        // Acción que deseas realizar cuando se pulsa el gatillo
        Debug.Log("Gatillo pulsado");
        if (estaDentro)
        {
            Debug.Log("Dentro de cubo");
            cubo.GetComponent<Renderer>().material.color = Color.red;
            panelInventario.SetActive(true);
        }
        else
        {
            Debug.Log("Fuera de cubo");
            cubo.GetComponent<Renderer>().material.color = Color.green;
            panelInventario.SetActive(false);
        }
    }

}
