using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Espada : MonoBehaviour
{

    [SerializeField ]private float danyo = 2f;  
    private Animator animator;
    private bool puedeAtacar;
    private bool atacando;
 
    void Start()
    {
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
        atacando = true;
        puedeAtacar = false;
        animator.SetTrigger("Atacar");
    }

    private void OnTriggerEnter(Collider objeto)
    {
        if (objeto.CompareTag("Enemigo") && atacando)
        {
            objeto.GetComponent<SaludEnemigoController>().RecibirDanyo(danyo);
        }
    }

    public void ResetAtaque()
    {
        puedeAtacar = true;
        atacando = false;
    }

}
