#if UNITY_EDITOR
using UnityEditor.ShaderKeywordFilter;
#endif
using UnityEngine;

public class LanzadorEventos : MonoBehaviour
{
    #region Variables
    [SerializeField] private AudioClip audioClip;
    private Camera camaraJugador;
    #endregion

    void Start()
    {
        camaraJugador = ConfiguracionJuego.instance.CamaraPrincipal;
    }

    private void ReproducirScreamer()
    {
        camaraJugador.GetComponent<AudioSource>().loop = false;
        camaraJugador.GetComponent<AudioSource>().clip = audioClip;
        camaraJugador.GetComponent<AudioSource>().volume = .5f;
        camaraJugador.GetComponent<AudioSource>().Play();
    }

    private void ReproducirMusica()
    {
        camaraJugador.GetComponent<AudioSource>().loop = true;
        camaraJugador.GetComponent<AudioSource>().clip = audioClip;
        camaraJugador.GetComponent<AudioSource>().volume = .6f;
        camaraJugador.GetComponent<AudioSource>().Play();
    }

    private void OnTriggerEnter(Collider other)
    {
       if (other.CompareTag("Player") && this.CompareTag("Scream"))
        {
            ReproducirScreamer();
            Destroy(this);
        }

        if (other.CompareTag("Player") && this.CompareTag("Musica"))
        {
            ReproducirMusica();
            Destroy(this);
        }
    }
}
