using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ia_BasicController : MonoBehaviour
{
    [Header("Vision")]
    [SerializeField] private float rangoMaximo = 10f;
    [SerializeField] private float anguloVision = 45f;
    [SerializeField] private LayerMask objetosMask;
    private LayerMask layermaskJugador;

    [Header("MoviemientoYAcciones")]
    [SerializeField] private float velocidadMovimiento = 10f;
    [SerializeField] private float distanciaSeguimiento = 0f;
    [SerializeField] private float distanciaAtaque = 0f;

    private GameObject jugador;

    private Animator animator;

    private bool atacando;

    void Start()
    {
        atacando = false;
        jugador = GameObject.FindGameObjectWithTag("Player").gameObject;
        layermaskJugador = jugador.layer;
    }

    void Update()
    {
        if (RangoDeVision())
        {
            PerseguirJugador();
            Debug.Log("JugadorDetectado");
        } 
        else
        {
            Debug.Log("No lo Detecta");
        }
            
    }

    private bool RangoDeVision()
    {
        Vector3 direccionJugador = jugador.transform.position - transform.position;
        float anguloEnemigoJugador = Vector3.Angle(transform.forward, direccionJugador);

        if(anguloEnemigoJugador < anguloVision * 0.5f && direccionJugador.magnitude <= rangoMaximo)
        {
            RaycastHit hit;
            if(Physics.Raycast(transform.position, direccionJugador.normalized, out hit, rangoMaximo, layermaskJugador)  &&
            !Physics.Raycast(transform.position, direccionJugador.normalized, rangoMaximo, objetosMask))
            {
                return true;
            }
        }

        return false;
    }

    private void PerseguirJugador()
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
