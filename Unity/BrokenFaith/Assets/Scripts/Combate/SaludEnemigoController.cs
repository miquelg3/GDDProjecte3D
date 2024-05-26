using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public class SaludEnemigoController : MonoBehaviour
{
    [SerializeField] float SaludBase = 100f;
    [SerializeField] private AudioClip muerte;

    private Animator animator;
    private CapsuleCollider collider;
    private NavMeshAgent agente;

    void Start()
    {
        animator = GetComponent<Animator>();
        collider = GetComponent<CapsuleCollider>();
        agente = GetComponent <NavMeshAgent>();
    }

    public void RecibirDanyo(float danyo)
    {
        SaludBase -= danyo;
        if (SaludBase <= 0) Muerte();
    }

    public void ReproducirMuerteAudio()
    {
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().loop = false;
        GetComponent<AudioSource>().clip = muerte;
        GetComponent<AudioSource>().Play();
    }

    public void Muerte()
    {
        ReproducirMuerteAudio();
        agente.isStopped = true;
        collider.enabled = false;
        GetComponent<EnemigoBasico>().enabled = false;
        animator.SetTrigger("muerto");
    }
}
