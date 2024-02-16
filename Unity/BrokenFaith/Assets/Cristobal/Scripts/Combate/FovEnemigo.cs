using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FovEnemigo : MonoBehaviour
{
    #region Variables

    [SerializeField] private float rangoMaximo = 10f;
    [SerializeField] private float anguloVision = 45f;
    [SerializeField] private LayerMask layermaskObjeto;
    [SerializeField] private LayerMask layermaskJugador;

    private float NivelDeAlerta;
    private float RangoAudicion;
    private float luminosidad;

    private GameObject jugador;

    private bool detectado;

    #endregion

    void Start()
    {
        RangoAudicion = 1000f;
        NivelDeAlerta = 0;
        jugador = GameObject.FindGameObjectWithTag("Player");
        detectado = false;
    }

    
    void Update()
    {
        luminosidad = MeasureLightIntensity(jugador.transform.position);
        Debug.Log(luminosidad);
        if (jugador.GetComponent<PlayerMovement>().agachado == true && rangoMaximo > 5f)
            rangoMaximo /= 2;
        else if (jugador.GetComponent<PlayerMovement>().agachado == false && rangoMaximo != 10f)
            rangoMaximo = 10f;

        if (NivelDeAlerta >= 10f)
            detectado = true;
    }

    private bool RangoDeVision()
    {
        Vector3 direccionJugador = jugador.transform.position - transform.position;
        float anguloEnemigoJugador = Vector3.Angle(transform.forward, direccionJugador.normalized);

        if (anguloEnemigoJugador < anguloVision * 0.5f && direccionJugador.magnitude <= rangoMaximo)
        {
            int layerMask = LayerMask.GetMask("Objeto", "Player");
        
            if (Physics.Raycast(transform.position, direccionJugador.normalized, out RaycastHit hit, rangoMaximo, layerMask))
            {
                if (hit.collider.gameObject.name.Equals("Player"))
                {
                    NivelDeAlerta += Time.deltaTime;
                    return true;
                }
            }


        }
        if (NivelDeAlerta > 0)
            NivelDeAlerta -= Time.deltaTime;
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        float halfFOV = anguloVision * 0.5f;
        Vector3 principio = Quaternion.Euler(0, -halfFOV, 0) * transform.forward * rangoMaximo;
        Vector3 final = Quaternion.Euler(0, halfFOV, 0) * transform.forward * rangoMaximo;

        Gizmos.DrawLine(transform.position, transform.position + principio);
        Gizmos.DrawLine(transform.position, transform.position + final);

        Gizmos.DrawLine(transform.position + principio, transform.position + final);
    }

    public bool GetDetectado()
    {
        return detectado;
    }

    float MeasureLightIntensity(Vector3 position)
    {
        float intensity = RenderSettings.ambientLight.grayscale; // Usa el nivel de luz ambiental como base
                                                                 // Ajusta esta lógica para calcular la intensidad de otras fuentes de luz si es necesario

        // Considera añadir aquí la lógica para calcular la intensidad de las fuentes de luz cercanas

        return intensity;
    }

}
