using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ia_BasicController : MonoBehaviour
{

    private GameObject jugador;

    [SerializeField] private float velocidadMoviemiento;
    [SerializeField] private float distanciaSeguimiento;
    [SerializeField] private float distanciaAtaque;

    private bool atacando;
    private float velActual;

    private Animator animator;

    void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        velActual = velocidadMoviemiento;
        atacando = false;
    }

    void Update()
    {
        float distanciaObjetivo = Vector3.Distance(jugador.transform.position, transform.position);

        if (distanciaObjetivo < distanciaSeguimiento) SeguirJugador();

        if (distanciaObjetivo < distanciaAtaque) Atacar();
    }

    public void SeguirJugador()
    {
        velocidadMoviemiento = velActual;
        Vector3 pos = transform.position;
        transform.position = 
            Vector3.MoveTowards(pos, pos - jugador.transform.position, velocidadMoviemiento * Time.deltaTime );
        atacando = false;
    }

    public void Atacar()
    {
        atacando = true;
        velocidadMoviemiento= 0;
        animator.SetBool("Atacando", atacando);
    }

}
