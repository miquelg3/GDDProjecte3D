using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arco : MonoBehaviour
{
    [SerializeField] private Transform spawnFlechas;
    [SerializeField] private Flecha flechaPrefab;

    [SerializeField]private float fuerzaBase = 2;
    private Flecha flechaActual;

    [SerializeField] private int cantidadFlechas = 10;

    [SerializeField] private float fuerzaMaxima = 70;
    private float fuerzaActual = 0;

    private bool cargando;

    public delegate void CambiarCantidad(int cantidad);
    public static event CambiarCantidad cantidadDeFlechas;

    public delegate void CambiarFuerza(float fuerza);
    public static event CambiarFuerza cambiarFuerza;


    void Start()
    {
        cantidadDeFlechas?.Invoke(cantidadFlechas);
        if (cantidadFlechas > 0)
            CrearFlecha();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && cantidadFlechas > 0)
        {
            cargando = true;
            flechaActual.Cargar();
        }
           
        if(cargando && fuerzaActual < fuerzaMaxima)
        {
            fuerzaActual += Time.deltaTime * 50f;
            cambiarFuerza?.Invoke(fuerzaActual);
        }
            
        if (cargando && Input.GetMouseButtonUp(0))
        {
            Disparar(fuerzaActual * fuerzaBase);
            cargando = false;
            fuerzaActual = 0;
            cambiarFuerza?.Invoke(fuerzaActual);
        }           
    }

    private void Disparar(float fuerza)
    {
        Vector3 fuerzaLanzada = spawnFlechas.TransformDirection(Vector3.forward * fuerza);
        flechaActual.Lanzar(fuerzaLanzada);
        cantidadFlechas--;
        cantidadDeFlechas?.Invoke(cantidadFlechas);

        if (cantidadFlechas > 0)
            CrearFlecha();            
    }

    private void CrearFlecha()
    {
        flechaActual = Instantiate(flechaPrefab, spawnFlechas);
        flechaActual.transform.localPosition = Vector3.zero;
    }
}
