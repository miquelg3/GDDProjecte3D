using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puertas : MonoBehaviour
{
    public float anguloApertura = 90f; 
    public float velocidadApertura = 2f; 

    private bool abriendo = false;
    private bool abierta;
    private Quaternion rotacionCerrada;
    private Quaternion rotacionAbierta;

    // Start is called before the first frame update
    void Start()
    {
        abierta = false;
        rotacionCerrada = transform.rotation;
        rotacionAbierta = rotacionCerrada * Quaternion.Euler(0, anguloApertura, 0);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {
        MovimientoJugador.AbrirPuerta += Abrir;
    }
    private void OnDisable()
    {
        MovimientoJugador.AbrirPuerta -= Abrir;
    }
    private void Abrir(Transform PosicionObjeto)
    {
        if (transform.position == PosicionObjeto.position)
        {
            Debug.Log(transform.rotation.y);
            if (!abriendo && transform.rotation.y <= 0)
            {
                StopAllCoroutines();
                StartCoroutine(AbrirCerrarPuerta(rotacionAbierta));
            }
            else if (!abriendo && transform.rotation.y > 0)
            {
                StartCoroutine(AbrirCerrarPuerta(rotacionCerrada));
            }
        }
        
    }

    private IEnumerator AbrirCerrarPuerta(Quaternion rotacionObjetivo)
    {
        abriendo = true;
        Quaternion rotacionInicial = transform.rotation;
        float tiempoTranscurrido = 0;

        while (tiempoTranscurrido < velocidadApertura)
        {
            transform.rotation = Quaternion.Lerp(rotacionInicial, rotacionObjetivo, tiempoTranscurrido / velocidadApertura);
            tiempoTranscurrido += Time.deltaTime;
            yield return null;
        }

        transform.rotation = rotacionObjetivo;
        abriendo = false;
        abierta = rotacionObjetivo == rotacionAbierta;
    }
}
