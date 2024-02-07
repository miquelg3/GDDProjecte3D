using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoBasico : MonoBehaviour
{
    #region Variables

        
    [SerializeField] private float velocidadMovimiento = 10f;        
    [SerializeField] private float distanciaAtaque = 0f;

    private Animator animator;
    private FovEnemigo fovEnemigo;

    private GameObject jugador;

    #endregion

    void Start()
    {
        animator = GetComponent<Animator>();
        fovEnemigo = GetComponent<FovEnemigo>();
        jugador = jugador = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (fovEnemigo.GetDetectado())
        {
            PerseguirJugador();
        }
    }

    private void PerseguirJugador()
    {
      transform.position = Vector3.MoveTowards(transform.position,jugador.transform.position, velocidadMovimiento * Time.deltaTime);
    }

    private void Atacar()
    {

    }
}
