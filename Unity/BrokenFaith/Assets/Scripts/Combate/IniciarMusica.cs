using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IniciarMusica : MonoBehaviour
{
    [SerializeField] private Camera camaraMain;
    [SerializeField] private AudioClip clipMusica;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            camaraMain.GetComponent<AudioSource>().loop = true;
            camaraMain.GetComponent<AudioSource>().clip = clipMusica;
            camaraMain.GetComponent<AudioSource>().Play();
            this.enabled = false;
        }
    }
}
