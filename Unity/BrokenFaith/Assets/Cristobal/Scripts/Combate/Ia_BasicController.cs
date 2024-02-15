using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ia_BasicController : MonoBehaviour
{
    [Header("Vision")]
    [SerializeField] private float rangoMaximo = 10f;
    [SerializeField] private float anguloVision = 45f;
    [SerializeField] private LayerMask layermaskObjeto;
    [SerializeField] private LayerMask layermaskJugador;

    [Header("MoviemientoYAcciones")]
    [SerializeField] private float velocidadMovimiento = 10f;
    [SerializeField] private float distanciaAtaque = 0f;

    private GameObject jugador;

    private Animator animator;
    private float NivelDeAlerta;

    void Start()
    {
        NivelDeAlerta = 0;
        jugador = GameObject.FindGameObjectWithTag("Player").gameObject;
        bool vision = jugador.GetComponent<PlayerMovement>().agachado;
        animator = GetComponent<Animator>();
        //layermaskJugador = jugador.layer;
        //Debug.Log(jugador.layer);
    }

    void Update()
    {
        if (jugador.GetComponent<PlayerMovement>().agachado == true && rangoMaximo>5f)
            rangoMaximo /= 2;
        else if (jugador.GetComponent<PlayerMovement>().agachado == false && rangoMaximo != 10f)
            rangoMaximo = 10f;

        if (RangoDeVision())
            Debug.Log("lo Detecta");
        else
            Debug.Log("No lo Detecta");

        //Debug.Log(NivelDeAlerta);
       //if (detectado) PerseguirJugador();
       if (NivelDeAlerta >= 10f)
            PerseguirJugador();
        
        
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
                NivelDeAlerta += Time.deltaTime;
                return true;
               
            }
        }
        if(NivelDeAlerta>0)
        NivelDeAlerta -= Time.deltaTime;
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
