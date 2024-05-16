using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Espada : MonoBehaviour
{
    [Header("Configuracion")]
    [SerializeField ]private float danyo = 2f;
    [SerializeField] private AudioClip audioSwing;
    [SerializeField] private AudioClip audioHit;
    [SerializeField] private AudioClip audioFocus;

    private Animator animator;
    private AudioSource audioSource;

    private bool puedeAtacar;
    private GameObject jugador;


    void Start()
    {
        jugador = ConfiguracionJuego.instance.Jugador;
        audioSource = GetComponent<AudioSource>();
        puedeAtacar = true;
        animator = GetComponent<Animator>(); 
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && puedeAtacar)
        {
            Atacar();
        }
    }

    public void Atacar()
    {
        audioSource.clip = audioSwing;
        audioSource.Play();
        puedeAtacar = false;
        animator.SetTrigger("Atacar");
    }

    private void OnTriggerEnter(Collider objeto)
    {
        audioSource.Stop();
        if (objeto.CompareTag("Enemigo"))
        {
            audioSource.clip = audioHit;
            audioSource.Play();
            objeto.GetComponent<SaludEnemigoController>().RecibirDanyo(danyo);
        }
    }

    public void ResetAtaque()
    {
        puedeAtacar = true;
    }

}
