using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagInteractable : MonoBehaviour
{
    public GameObject cubo;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "RightHand Controller")
        {
            Debug.Log("Dentro de cubo");
            cubo.GetComponent<Renderer>().material.color = Color.green;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "RightHand Controller")
        {
            Debug.Log("Dentro de cubo");
            cubo.GetComponent<Renderer>().material.color = Color.red;
        }
    }

    public void CambiarColor()
    {
        cubo.GetComponent<Renderer>().material.color = Color.blue;
    }
}
