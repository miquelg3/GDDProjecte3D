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
            txtCant.SetActive(true);
            txtFuerza.SetActive(true);
            crosshair.SetActive(true);
            espada.SetActive(false);
            arco.SetActive(false);
        } else if (other.tag == "Player" && mostrarEspada)
        {
            txtCant.SetActive(false);
            txtFuerza.SetActive(false);
            crosshair.SetActive(false);
            espada.SetActive(true);
            arco.SetActive(false);
        }
    }
   
}
