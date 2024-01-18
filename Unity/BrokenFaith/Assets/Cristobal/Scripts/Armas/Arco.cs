using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arco : MonoBehaviour
{
    [SerializeField] private Transform spawnFlechas;
    [SerializeField] private GameObject flechaPrefab;

    private GameObject flechaActual;

    private int cantidadFlechas = 10;

    public delegate void HudTexto(int cantidad);
    public static event HudTexto cantidadDeFlechas;


    void Start()
    {
        cantidadDeFlechas?.Invoke(cantidadFlechas);
        if (cantidadFlechas > 0)
            CrearFlecha();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && cantidadFlechas > 0)
            Disparar();
    }

    private void Disparar()
    {
        cantidadDeFlechas?.Invoke(--cantidadFlechas);
    }

    private void CrearFlecha()
    {
            flechaActual = Instantiate(flechaPrefab, spawnFlechas);
            flechaActual.transform.rotation = spawnFlechas.transform.localRotation;
            flechaActual.transform.localPosition = Vector3.zero; 
    }
}
