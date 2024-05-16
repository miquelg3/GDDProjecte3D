using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemigoBasico : MonoBehaviour
{
    #region Variables

    [Header("Movieminto")]
    [SerializeField] private float rangoMovimiento = 10f;
    [SerializeField] private bool porPuntos;
    [SerializeField] Vector3[] puntosNavegacion;
    [SerializeField] private float tiempoEspera = 5f;

    private bool llegoADestino = false;
    private bool estaCaminando = false;
    private bool perseguir = false;

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
        jugador = ConfiguracionJuego.instance.Jugador;
        IniciarPatrullaje();
    }

    void Update()
    {
        if (fovEnemigo.GetDetectado())
        {
            perseguir = true;
            PerseguirJugador();
        }
        
        if (!fovEnemigo.GetDetectado() && !perseguir)
        {
            if (!llegoADestino)
            {
                if (porPuntos)
                    PatrullajePorPuntos();
                else
                    PatrullajeLibre();
            }

        }
           
        animator.SetBool("caminando", estaCaminando);
    }

    private void PerseguirJugador()
    {
        transform.LookAt(jugador.transform.position);

        Debug.Log("PERSIGUE");

        agente.SetDestination(jugador.transform.position);
        estaCaminando = true;

        if (Vector3.Distance(transform.position, jugador.transform.position) < 1f)
        {
            Debug.Log("Ataca");
            animator.SetBool("atacando", true);
            agente.velocity = Vector3.zero;
        }
        else
        {
            animator.SetBool("atacando", false);
            agente.SetDestination(jugador.transform.position);
        }
            

    }

    private void PatrullajePorPuntos()
    {
        animator.SetBool("atacando", false);
        if (puntosNavegacion.Length == 0) return;

        if (Vector3.Distance(transform.position, puntosNavegacion[indexPuntos]) < 1f)
        {
            if (!llegoADestino)
            {
                llegoADestino = true;
                estaCaminando = false;
                Invoke("CambiarPuntoPatrullaje", tiempoEspera);
            }
        }
        else
        {
            agente.SetDestination(puntosNavegacion[indexPuntos]);
            estaCaminando = true;
        }
    }

    private void PatrullajeLibre()
    {
        animator.SetBool("atacando", false);
        if (Vector3.Distance(transform.position, agente.destination) < 1f)
        {
            if (!llegoADestino)
            {
                llegoADestino = true;
                estaCaminando = false;
                Invoke("BuscarNuevoDestino", tiempoEspera);
            }
        }
    }

    private void BuscarNuevoDestino()
    {
        llegoADestino = false;
        Vector3 randomDirection = Random.insideUnitSphere * rangoMovimiento;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, rangoMovimiento, 1);
        Vector3 finalPosition = hit.position;
        agente.SetDestination(finalPosition);
        estaCaminando = true;
    }

    private void IniciarPatrullaje()
    {
        estaCaminando = true;
        if (porPuntos && puntosNavegacion.Length > 0)
        {
            agente.SetDestination(puntosNavegacion[indexPuntos]);
        }
        else
        {
            BuscarNuevoDestino();
        }
    }

    private void CambiarPuntoPatrullaje()
    {
        llegoADestino = false;
        indexPuntos = (indexPuntos + 1) % puntosNavegacion.Length;
    }


}
