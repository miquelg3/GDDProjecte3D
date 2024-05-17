using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombateEspada : MonoBehaviour
{

    #region Variables

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioSwing;
    private Animator animator;
    private bool puedeAtacar;

    #endregion
    void Start()
    {
        puedeAtacar = true;
        animator = GetComponent<Animator>();
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && puedeAtacar) Atacar();

        if (Input.GetKeyDown(KeyCode.Mouse1)) Defensa();
    }

    public void Atacar()
    {
        audioSource.clip = audioSwing;
        audioSource.Play();
        puedeAtacar = false;
        animator.SetTrigger("Atacar");
    }

    public void Defensa()
    {
        animator.SetBool("Defensa", true);
    }

    public void ResetAtaque()
    {
        puedeAtacar = true;
    }
}
