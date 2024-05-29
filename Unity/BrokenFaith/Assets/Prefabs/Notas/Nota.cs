using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Nota : MonoBehaviour
{
    private Vector3 escala;
    private void Start()
    {
        escala = transform.localScale;
    }
    private void OnEnable()
    {
        MovimientoJugador.RecogerNota += Recoger;
    }

    private void OnDisable()
    {
        MovimientoJugador.RecogerNota -= Recoger;
    }

    private void Recoger(Transform nota)
    {
        if (transform.position == nota.position)
        {
            Debug.Log("Nota guardada");
            Transform notaPos = ConfiguracionJuego.instance.Jugador.transform.GetChild(ConfiguracionJuego.instance.Jugador.transform.childCount - 1);
            nota.SetParent(notaPos);
            //nota.gameObject.SetActive(false);
            nota.position = notaPos.position;
            nota.rotation = notaPos.rotation;
            nota.localScale = escala*5;
            Destroy(nota.gameObject.GetComponent<XRGrabInteractable>());
            Destroy(nota.gameObject.GetComponent<Rigidbody>());
            nota.gameObject.SetActive(false);
            //nota.position = ConfiguracionJuego.instance.Jugador.transform.position;
            InventarioScript.instance.GuardarNota(nota.gameObject);
        }
    }
}
