using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PruebaCollision : MonoBehaviour
{
    public TextMeshPro texto;
    // Start is called before the first frame update
    void Start()
    {
        texto.text = "Hola";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "LeftHandController")
            texto.text = "Entra la mano";
        else
            texto.text = "Entra algo";
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "LeftHandController")
            texto.text = "Sale";
        else
            texto.text = "Sale algo";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            texto.text = "La mano está entrando";
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            texto.text = "La mano está dentro";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            texto.text = "La mano ha salido del objeto";
        }
    }
}
