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

    void Start()
    {
        animator = GetComponent<Animator>();
        collider = GetComponent<CapsuleCollider>();
    }

    public void RecibirDanyo(float danyo)
    {
        SaludBase -= danyo;
        if (SaludBase <= 0) Muerte();
    }

    public void Muerte()
    {
        collider.enabled = false;
        animator.SetTrigger("Muerto");
    }
}
