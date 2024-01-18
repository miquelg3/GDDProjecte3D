using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HudController : MonoBehaviour
{
    public TextMeshProUGUI txtCantidadFlechas, txtFuerzaFlecha;

    void OnEnable()
    {
        Arco.cantidadDeFlechas += CantidadDeFlechas;
        Flecha.cambiarFuerza += CambiarFuerza;
    }

    void OnDisable()
    {
        Arco.cantidadDeFlechas -= CantidadDeFlechas;
        Flecha.cambiarFuerza -= CambiarFuerza;
    }

    public void CantidadDeFlechas(int cantidad)
    {
        txtCantidadFlechas.text = $"Flechas: {cantidad}";
    }

    public void CambiarFuerza(float fuerza)
    {
        txtFuerzaFlecha.text = $"Fuerza: {fuerza}";
    }

}
