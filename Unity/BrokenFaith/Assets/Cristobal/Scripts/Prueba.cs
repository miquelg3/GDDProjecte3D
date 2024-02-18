using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prueba : MonoBehaviour
{

    [SerializeField] private bool mostrarEspada;
    [SerializeField] private bool mostrarArco;

    public GameObject txtCant, txtFuerza, crosshair, espada, arco;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && mostrarArco)
        {
            txtCant.active = true;
            txtFuerza.active = true;
            crosshair.active = true;
            espada.active = false;
            arco.active = true;
        } else if (other.tag == "Player" && mostrarEspada)
        {
            txtCant.active = false;
            txtFuerza.active = false;
            crosshair.active = false;
            espada.active = true;
            arco.active = false;
        }
    }
   
}
