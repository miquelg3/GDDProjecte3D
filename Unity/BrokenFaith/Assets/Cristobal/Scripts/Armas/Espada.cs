using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Espada : MonoBehaviour
{

    [SerializeField ]private float danyo = 2f;  
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

    private void OnTriggerEnter(Collider objeto)
    {
        Debug.Log("Entra al collider.");
        if (objeto.CompareTag("Enemigo"))
        {
            Debug.Log("Colisión con enemigo detectada.");
            objeto.GetComponent<SaludEnemigoController>().RecibirDanyo(danyo);
        }
    }

    public void ResetAtaque()
    {
        puedeAtacar = true;
    }

}
