using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mueble : MonoBehaviour
{
    private Vector3 bloqueo;
    private Vector3 PasoAbierto;
    private bool Desbloqueado;
    public float velocidadApertura = 2f;
    // Start is called before the first frame update
    void Start()
    {
        Desbloqueado = false;
        bloqueo = transform.position;
        PasoAbierto = new Vector3(bloqueo.x, bloqueo.y, bloqueo.z - 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {
        MovimientoJugador.QuitarMueble += Desbloquear;
    }
    private void OnDisable()
    {
        MovimientoJugador.QuitarMueble -= Desbloquear;
    }
    private void Desbloquear()
    {
       if (!Desbloqueado) 
        {
            StartCoroutine(ApartarMuebleErmita());
        }

    }

    private IEnumerator ApartarMuebleErmita()
    {
        float tiempoTranscurrido = 0;

        while (tiempoTranscurrido < velocidadApertura)
        {
            transform.position = Vector3.Lerp(bloqueo, PasoAbierto, tiempoTranscurrido / velocidadApertura);
            tiempoTranscurrido += Time.deltaTime;
            yield return null;
        }
        Desbloqueado = true;
    }
}
