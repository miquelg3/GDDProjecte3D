using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IniciarMusica : MonoBehaviour
{
    [SerializeField] private Camera camaraMain;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            camaraMain.GetComponent<AudioSource>().Play();
        }
    }
}
