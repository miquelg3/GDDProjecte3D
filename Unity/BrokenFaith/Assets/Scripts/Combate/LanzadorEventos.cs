#if UNITY_EDITOR
using UnityEditor.ShaderKeywordFilter;
#endif
using UnityEngine;

public class LanzadorEventos : MonoBehaviour
{
    #region Variables
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private Camera camaraJugador;
    #endregion

    private void Reproducir()
    {
        camaraJugador.GetComponent<AudioSource>().clip = audioClip;
        camaraJugador.GetComponent<AudioSource>().volume = .5f;
        camaraJugador.GetComponent<AudioSource>().Play();
    }

    private void OnTriggerEnter(Collider other)
    {
       if (other.CompareTag("Player"))
        {
            Reproducir();
            Destroy(this);
        }
    }
}
