using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemigoBasico : MonoBehaviour
{
    #region Variables

    [Header("Movieminto")]
    [SerializeField] private float rangoMovimiento = 10f;
    private Vector3 puntosPatrullaje;
    private bool llegoADestino;

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
        llegoADestino = true; ;
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
        if (!llegoADestino) agente.SetDestination(puntosPatrullaje);
        if (llegoADestino) CrearPuntoNuevo();
        if (Vector3.Distance(transform.position, puntosPatrullaje) < 3f) llegoADestino = true;
    }

    private void CrearPuntoNuevo()
    {
        float x = Random.Range(-rangoMovimiento, rangoMovimiento);
        float z = Random.Range(-rangoMovimiento, rangoMovimiento);

        puntosPatrullaje = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);

        llegoADestino = false;

    }

    private void Atacar()
    {
        Debug.Log("Atacando");
    }
}
