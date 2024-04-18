using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FovEnemigo : MonoBehaviour
{
    #region Variables
    private float luminosidad;
    private float nivelDeAlerta;

    private GameObject jugador;

    private bool detectado;
    #endregion

    void Start()
    {
        ConfiguracionJuego.instance.RangoAudicion = 1000;
        nivelDeAlerta = 0;
        jugador = GameObject.FindGameObjectWithTag("Player");
        detectado = false;
 
    }

    
    void Update()
    {
        luminosidad = MeasureLightIntensity(jugador.transform.position);
        if (jugador.GetComponent<MovimientoJugador>().agachado == true && ConfiguracionJuego.instance.RangoMaximo > 5f)
            ConfiguracionJuego.instance.RangoMaximo /= 2;
        else if (jugador.GetComponent<MovimientoJugador>().agachado == false 
            && ConfiguracionJuego.instance.RangoMaximo != 10f)
            ConfiguracionJuego.instance.RangoMaximo = 20f;

        Debug.Log(detectado);
        detectado = RangoDeVision();
    }

    private bool RangoDeVision()
    {
        Vector3 direccionJugador = jugador.transform.position - transform.position;
        float anguloEnemigoJugador = Vector3.Angle(transform.forward, direccionJugador.normalized);

        if (anguloEnemigoJugador < ConfiguracionJuego.instance.AnguloVision * 0.5f 
            && direccionJugador.magnitude <= ConfiguracionJuego.instance.RangoMaximo)
        {
            int layerMask = LayerMask.GetMask("Objeto", "Player");

            if (Physics.Raycast(transform.position, direccionJugador.normalized, 
                out RaycastHit hit, ConfiguracionJuego.instance.RangoMaximo, layerMask))
            {
                if (hit.collider.gameObject.name.Equals("Player"))
                {
                    nivelDeAlerta += Time.deltaTime;
                    return true;
                }
            }


        }
        if (nivelDeAlerta > 0) nivelDeAlerta -= Time.deltaTime;
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        float halfFOV = ConfiguracionJuego.instance.AnguloVision * 0.5f;

        Vector3 principio = Quaternion.Euler(0, -halfFOV, 0) 
            * transform.forward * ConfiguracionJuego.instance.RangoMaximo;
        Vector3 final = Quaternion.Euler(0, halfFOV, 0) 
            * transform.forward * ConfiguracionJuego.instance.RangoMaximo;

        Gizmos.DrawLine(transform.position, transform.position + principio);
        Gizmos.DrawLine(transform.position, transform.position + final);

        Gizmos.DrawLine(transform.position + principio, transform.position + final);
    }

    public bool GetDetectado()
    {
        return detectado;
    }

    private void OnEnable()
    {
        SoundEventManager.OnSoundEvent += OnSoundEvent;
    }

    private void OnDisable()
    {
        SoundEventManager.OnSoundEvent -= OnSoundEvent;
    }

    private void OnSoundEvent(SoundEvent soundEvent)
    {
        if (Vector3.Distance(transform.position, soundEvent.soundPosition) 
            <= ConfiguracionJuego.instance.RangoAudicion)
        {
            Debug.Log("Enemigo escuchó un sonido proveniente de " + soundEvent.soundPosition);
        }
    }
    float MeasureLightIntensity(Vector3 position)
    {
        float intensity = RenderSettings.ambientLight.grayscale; // Usa el nivel de luz ambiental como base
                                                                 // Ajusta esta lógica para calcular la intensidad de otras fuentes de luz si es necesario

        // Considera añadir aquí la lógica para calcular la intensidad de las fuentes de luz cercanas

        return intensity;
    }


}
