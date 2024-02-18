using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Espada : MonoBehaviour
{

    [SerializeField ]private float danyo = 2f;

    [SerializeField] private AudioClip audioSwing;
    [SerializeField] private AudioClip audioHit;
    [SerializeField] private AudioClip audioFocus;

    private Animator animator;
    private AudioSource audioSource;

    private bool puedeAtacar;
    private bool atacando;
 
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        puedeAtacar = true;
        atacando = false;
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
        atacando = true;
        puedeAtacar = false;
        animator.SetTrigger("Atacar");
    }

    private void OnTriggerEnter(Collider objeto)
    {
        audioSource.Stop();
        if (objeto.CompareTag("Enemigo") && atacando)
        {
            audioSource.clip = audioHit;
            audioSource.Play();
            objeto.GetComponent<SaludEnemigoController>().RecibirDanyo(danyo);
        }
    }

    public void ResetAtaque()
    {
        puedeAtacar = true;
        atacando = false;
    }

}
