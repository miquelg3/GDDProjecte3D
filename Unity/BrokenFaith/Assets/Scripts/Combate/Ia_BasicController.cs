using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ia_BasicController : MonoBehaviour
{
    [Header("Vision")]
    [SerializeField] private float rangoMaximo = 10f;
    [SerializeField] private float anguloVision = 45f;
    [SerializeField] private LayerMask layermaskObjeto;
    [SerializeField] private LayerMask layermaskJugador;

    [Header("MoviemientoYAcciones")]
    [SerializeField] private float velocidadMovimiento = 10f;
    [SerializeField] private float distanciaAtaque = 0f;
    private float RangoAudicion;
    private float luminosidad;

    private GameObject jugador;

    private Animator animator;
    private float NivelDeAlerta;

    void Start()
    {
        RangoAudicion = 1000f;
        NivelDeAlerta = 0;
        jugador = GameObject.FindGameObjectWithTag("Player").gameObject;
        bool vision = jugador.GetComponent<MovimientoJugador>().agachado;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        luminosidad = MeasureLightIntensity(jugador.transform.position);
        Debug.Log(luminosidad);
        if (jugador.GetComponent<MovimientoJugador>().agachado == true && rangoMaximo>5f)
            rangoMaximo /= 2;
        else if (jugador.GetComponent<MovimientoJugador>().agachado == false && rangoMaximo != 10f)
            rangoMaximo = 10f;

       /** if (RangoDeVision())
            Debug.Log("lo Detecta");
        else
            Debug.Log("No lo Detecta");*/
       if (NivelDeAlerta >= 10f)
            PerseguirJugador();
        
        
    }

    private bool RangoDeVision()
    {
        Vector3 direccionJugador = jugador.transform.position - transform.position;
        float anguloEnemigoJugador = Vector3.Angle(transform.forward, direccionJugador.normalized);

        if(anguloEnemigoJugador < anguloVision * 0.5f && direccionJugador.magnitude <= rangoMaximo)
        {
            int layerMask = LayerMask.GetMask("Objeto", "Player");
            RaycastHit hit;

            if(Physics.Raycast(transform.position, direccionJugador.normalized,out hit, rangoMaximo, layerMask))
            {
                if (hit.collider.gameObject.name.Equals("Player"))
                {
                    NivelDeAlerta += Time.deltaTime;
                    return true;
                }
            }
                
               
        }
        if(NivelDeAlerta>0)
        NivelDeAlerta -= Time.deltaTime;
        return false;
    }

    private void PerseguirJugador()
    {
        float distancia = Vector3.Distance(transform.position, jugador.transform.position);
        Vector3 posicion = transform.position;

        if(distancia > distanciaAtaque)
        {
            transform.position = Vector3.MoveTowards(posicion, posicion - jugador.transform.position, velocidadMovimiento * Time.deltaTime);
        }

        if(distancia < distanciaAtaque)
        {
            Debug.Log("Atacando");
            Atacar();
        }
    }

    private void Atacar()
    {

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
        if (Vector3.Distance(transform.position, soundEvent.soundPosition) <= RangoAudicion)
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
