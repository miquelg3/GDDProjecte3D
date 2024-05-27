using UnityEngine;

public class Espada : MonoBehaviour
{
    [Header("Configuracion")]
    [SerializeField ]private float danyo = 2f;
    [SerializeField] private AudioClip audioHit;
    private CombateEspada combateEspada;



    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        combateEspada = GameObject.FindGameObjectWithTag("Espada").GetComponent<CombateEspada>();
    }

    private void OnTriggerEnter(Collider objeto)
    {
        if (combateEspada.GetAtacando() && (objeto.CompareTag("Enemigo") || objeto.CompareTag("Jefe1")))
        {
            audioSource.Stop();
            audioSource.clip = audioHit;
            audioSource.Play();

            objeto.GetComponent<SaludEnemigoController>().RecibirDanyo(danyo);
        }
    }
}
