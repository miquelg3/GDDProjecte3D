using System;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public class SaludEnemigoController : MonoBehaviour
{
    #region Variables
    [SerializeField] float SaludBase = 100f;
    [SerializeField] private AudioClip muerte;

    private Animator animator;
    private new CapsuleCollider collider;
    private NavMeshAgent agente;

    private bool estaMuerto;
    #endregion

    void Start()
    {
        animator = GetComponent<Animator>();
        collider = GetComponent<CapsuleCollider>();
        agente = GetComponent <NavMeshAgent>();
        estaMuerto = false;
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
        estaMuerto = true;
        ReproducirMuerteAudio();
        agente.isStopped = true;
        collider.enabled = false;
        GetComponent<EnemigoBasico>().enabled = false;
        animator.SetTrigger("muerto");
    }

    public bool EstaMuerto
    {
        get { return estaMuerto; }
    }
}
