using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arco : MonoBehaviour
{
    #region variables
    [SerializeField] private Transform spawnFlechas;
    [SerializeField] private Flecha flechaPrefab;

    [SerializeField]private float fuerzaBase = 2;
    private Flecha flechaActual;

    [SerializeField] private int cantidadFlechas = 10;

    [SerializeField] private float fuerzaMaxima = 70;
    private float fuerzaActual = 0;

    [SerializeField] private AudioClip audioLanzar;
    [SerializeField] private AudioClip audioCargar;

    private AudioSource audioSource;

    private bool cargando;

    public delegate void CambiarCantidad(int cantidad);
    public static event CambiarCantidad cantidadDeFlechas;

    public delegate void CambiarFuerza(float fuerza);
    public static event CambiarFuerza cambiarFuerza;
    private GameObject jugador;
    private Animator animator;
    #endregion

    void Start()
    {
        jugador = GameObject.FindWithTag("Player");
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();

        cantidadDeFlechas?.Invoke(cantidadFlechas);

        //CAmbiar esto para cuando este listo lo de inventario, Saber a que tengo que acceder
        if (cantidadFlechas > 0) 
            CrearFlecha();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && cantidadFlechas > 0)
        {
            cargando = true;
            animator.SetBool("Cargando", cargando);
            flechaActual.Cargar();
            ReproducirSonidoNuevo(audioCargar);
            SoundEventManager.EmitSoundEvent(new SoundEvent(jugador.transform.position, 1500f));
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
            animator.SetBool("Cargando", cargando);
            fuerzaActual = 0;
            cambiarFuerza?.Invoke(fuerzaActual);
        }           
    }

    private void Disparar(float fuerza)
    {
        Vector3 fuerzaLanzada = spawnFlechas.TransformDirection(Vector3.forward * fuerza);
        flechaActual.Lanzar(fuerzaLanzada);
        ReproducirSonidoNuevo(audioLanzar);
        SoundEventManager.EmitSoundEvent(new SoundEvent(jugador.transform.position, 1500f));

        cantidadFlechas--;
        cantidadDeFlechas?.Invoke(cantidadFlechas);

        CrearFlecha();        
    }

    public void ReproducirSonidoNuevo(AudioClip nuevoAudio)
    {
        audioSource.Stop();
        audioSource.clip = nuevoAudio;
        audioSource.Play();
    }

    private void CrearFlecha()
    {
        if (cantidadFlechas > 0)
        {
            flechaActual = Instantiate(flechaPrefab, spawnFlechas);
            flechaActual.transform.localPosition = Vector3.zero;
        }
    }
}
