using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemigoBasico : MonoBehaviour
{
    #region Variables

    [Header("Moviemiento")] 
    [SerializeField] private float velocidadMovimiento = 10f;        
    [SerializeField] private float distanciaAtaque = 0f;

    [Header("Puntos")]
    [SerializeField] List<Vector3> puntos;
    private Vector3 posIncial;
    private int index;

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
        posIncial = puntos[0];
    }

    void Update()
    {
        if (fovEnemigo.GetDetectado()) PerseguirJugador();
        else Patrullar();
    }

    private void PerseguirJugador()
    {
        agente.SetDestination(jugador.transform.position);
    }

    private void Patrullar()
    {
        if(puntos != null)
        {
            index = 0;

            if (index > 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, jugador.transform.position, velocidadMovimiento * Time.deltaTime);

                if (Vector3.Distance(transform.position, puntos[index]) < 0.1f) index = (index + 1) % puntos.Count;
            }
            else transform.position = Vector3.MoveTowards(transform.position, posIncial, velocidadMovimiento * Time.deltaTime);
        }
    }

    private void Atacar()
    {

    }
}
