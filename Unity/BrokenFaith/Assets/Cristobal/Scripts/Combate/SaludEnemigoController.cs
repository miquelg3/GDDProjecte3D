using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaludEnemigoController : MonoBehaviour
{
    [SerializeField] float SaludBase = 100f;

    private Animator animator;
    private new CapsuleCollider collider;
    private bool muerto;

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
        animator.SetTrigger("Muerto");
    }
}
