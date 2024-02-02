using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ia_BasicController : MonoBehaviour
{
    [Header("Vision")]
    [SerializeField] private float rangoMaximo = 10f;
    [SerializeField] private float anguloVision = 45f;
    [SerializeField] private LayerMask layermaskObjeto;
    private LayerMask layermaskJugador;

    [Header("MoviemientoYAcciones")]
    [SerializeField] private float velocidadMovimiento = 10f;
    [SerializeField] private float distanciaAtaque = 0f;

    private GameObject jugador;

    private Animator animator;

    private bool detectado;

    void Start()
    {
        detectado = false;
        jugador = GameObject.FindGameObjectWithTag("Player").gameObject;
        animator = GetComponent<Animator>();
        layermaskJugador = jugador.layer;
        Debug.Log(jugador.layer);
    }

    void Update()
    {

        if (RangoDeVision())
            Debug.Log("lo Detecta");
        else
            Debug.Log("No lo Detecta");


       //if (detectado) PerseguirJugador();
    }

    private bool RangoDeVision()
    {
        Vector3 direccionJugador = jugador.transform.position - transform.position;
        float anguloEnemigoJugador = Vector3.Angle(transform.forward, direccionJugador.normalized);

        if(anguloEnemigoJugador < anguloVision * 0.5f && direccionJugador.magnitude <= rangoMaximo)
        {
            RaycastHit hit;

            Debug.DrawRay(transform.position, direccionJugador.normalized, Color.blue, rangoMaximo);

            if (Physics.Raycast(transform.position, direccionJugador.normalized, rangoMaximo, layermaskJugador)  &&
                !Physics.Raycast(transform.position, direccionJugador.normalized, rangoMaximo, layermaskObjeto))
            {
                return true;
            }
        }

        return false;
    }

    private void PerseguirJugador()
    {
        float distancia = Vector3.Distance(transform.position, jugador.transform.position);
        Vector3 posicion = transform.position;

        if(distancia > distanciaAtaque)
        {
            transform.position = Vector3.MoveTowards(posicion, posicion - jugador.transform.position, velocidadMovimiento * Time.deltaTime);
        }

        if(distancia < distanciaAtaque)
        {
            Debug.Log("Atacando");
            Atacar();
        }
    }

    private void Atacar()
    {

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        float halfFOV = anguloVision * 0.5f;
        Vector3 principio = Quaternion.Euler(0, -halfFOV, 0) * transform.forward * rangoMaximo;
        Vector3 final = Quaternion.Euler(0, halfFOV, 0) * transform.forward * rangoMaximo;

        Gizmos.DrawLine(transform.position, transform.position + principio);
        Gizmos.DrawLine(transform.position, transform.position + final);

        Gizmos.DrawLine(transform.position + principio, transform.position + final);
    }

}
