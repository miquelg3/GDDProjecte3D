using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Espada34 : MonoBehaviour
{

    private float danyo = 10f;  
    private Animator animator;
    private bool puedeAtacar;
 
    void Start()
    {
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

    public void ResetAtaque()
    {
        puedeAtacar = true;
    }

}
