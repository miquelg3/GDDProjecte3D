using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Espada : MonoBehaviour
{

    private float danyo = 10f;  
    private Animator animator;
    private bool puedeAtacar;
    private float tiempoAtacar;



    void Start()
    {
        tiempoAtacar = 1f;
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
        puedeAtacar = false;
        animator.SetTrigger("Atacar");
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("SI");
    }


    public void ResetAtaque()
    {
        puedeAtacar = true;
    }

}
