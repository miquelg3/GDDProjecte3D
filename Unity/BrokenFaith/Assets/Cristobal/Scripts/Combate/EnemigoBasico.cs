using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemigoBasico : MonoBehaviour
{
    #region Variables

    [Header("Movieminto")]
    [SerializeField] private float rangoMovimiento = 10f;
    private Vector3 puntoPatrullaje;
    private bool llegoADestino;

    [SerializeField] private bool porPuntos;
    [SerializeField] Vector3[] puntosNavegacion;
    [SerializeField] private float tiempoEspera = 5f;
    private float tiempoEsperado = 0f ;
    private int indexPuntos = 0;


    private NavMeshAgent agente;


    private Animator animator;
    private FovEnemigo fovEnemigo;

    private GameObject jugador;

    #endregion

    void Start()
    {
        animator = GetComponent<Animator>();
        fovEnemigo = GetComponent<FovEnemigo>();
        agente = GetComponent<NavMeshAgent>();
        jugador = jugador = GameObject.FindGameObjectWithTag("Player");
        llegoADestino = true; 
    }

    void Update()
    {
        if (fovEnemigo.GetDetectado()) PerseguirJugador();
        else Patrullar();       
    }

    private void PerseguirJugador()
    {
        agente.SetDestination(jugador.transform.position);

        transform.LookAt(jugador.transform);
    }

    private void Patrullar()
    {
        if (!porPuntos)
        {
            if (!llegoADestino) agente.SetDestination(puntoPatrullaje);
            if (llegoADestino) CrearPuntoNuevo();
            if (Vector3.Distance(transform.position, puntoPatrullaje) < 3f) llegoADestino = true;
        }

        if (porPuntos) PatrullajeDesignado();
    }

    private void PatrullajeDesignado()
    {
        if (puntosNavegacion == null) return;

        if (indexPuntos == 0)
        {
            agente.SetDestination(puntosNavegacion[indexPuntos]);
            animator.SetBool("estaCaminando", true);
        }
            
        if (Vector3.Distance(transform.position, puntosNavegacion[indexPuntos]) < 1f) 
           if(tiempoEsperado <= 0)
            {
                CambiarPuntoPatrullaje();
                tiempoEsperado = tiempoEspera;
            } else
            {
                
                tiempoEsperado -= Time.deltaTime;
            }
    }

    private void CambiarPuntoPatrullaje()
    {
        indexPuntos = (indexPuntos + 1) % puntosNavegacion.Length;
        agente.SetDestination(puntosNavegacion[indexPuntos]);
        animator.SetBool("estaCaminando", true);
    }

    private void CrearPuntoNuevo()
    {
        float x = Random.Range(-rangoMovimiento, rangoMovimiento);
        float z = Random.Range(-rangoMovimiento, rangoMovimiento);

        puntoPatrullaje = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);

        llegoADestino = false;

    }

    private void Atacar()
    {
        Debug.Log("Atacando");
    }
}
