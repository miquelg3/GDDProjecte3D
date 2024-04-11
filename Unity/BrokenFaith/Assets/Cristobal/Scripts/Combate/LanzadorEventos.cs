using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class LanzadorEventos : MonoBehaviour
{
    #region Variables
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private Camera camaraJugador;
    [SerializeField] private EnemigoBasico enemigoBasico;
    #endregion

    private void Reproducir()
    {
        camaraJugador.GetComponent<AudioSource>().clip = audioClip;
        camaraJugador.GetComponent<AudioSource>().volume = .5f;
        camaraJugador.GetComponent<AudioSource>().Play();
    }

    private void Destruir()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
       if (other.CompareTag("Player"))
        {
            Reproducir();
            Destruir();
        }
    }
}
