using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Espada : MonoBehaviour
{
    private Animator animator;
    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
        animator = GetComponent<Animator>();
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            animator.SetTrigger("Atacar");
        }
        transform.localScale = originalScale;
    }
}
