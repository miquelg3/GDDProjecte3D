using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HudController : MonoBehaviour
{
    public TextMeshProUGUI txtCantidadFlechas, txtFuerzaFlecha;

    void OnEnable()
    {
        Arco34.cantidadDeFlechas += CantidadDeFlechas;
        Arco34.cambiarFuerza += CambiarFuerza;
    }

    void OnDisable()
    {
        Arco34.cantidadDeFlechas -= CantidadDeFlechas;
        Arco34.cambiarFuerza -= CambiarFuerza;
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
