using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaludEnemigoController : MonoBehaviour
{
    [SerializeField] float SaludBase = 100f;

    private Transform transform { get; set; }

    private Animator animator;
    private CapsuleCollider collider;

    private Ia_BasicController ia_controller;

    private bool muerto { get; set; }

    void Start()
    {
        animator = GetComponent<Animator>();
        collider = GetComponent<CapsuleCollider>();
        muerto = false;
    }

    public void RecibirDanyo(float danyo)
    {
        SaludBase -= danyo;
        if (SaludBase <= 0) Muerte();
    }

    public void Muerte()
    {
        muerto = true;
        collider.enabled = false;
        //ia_controller.enabled = false;
        animator.SetTrigger("Muerto");
    }
}
