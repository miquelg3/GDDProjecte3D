using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Espada : MonoBehaviour
{
    [Header("Configuracion")]
    [SerializeField ]private float danyo = 2f;
    [SerializeField] private AudioClip audioHit;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider objeto)
    {
        audioSource.Stop();
        audioSource.clip = audioHit;
        audioSource.Play();

        if (objeto.CompareTag("Enemigo"))
        {
            objeto.GetComponent<SaludEnemigoController>().RecibirDanyo(danyo);
        }
    }
}
