using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Espada : MonoBehaviour
{

    private float danyo = 10f;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>(); 
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            animator.SetTrigger("Atacar");
        }
    }
}
