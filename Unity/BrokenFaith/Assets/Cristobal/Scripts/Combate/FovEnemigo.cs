using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FovEnemigo : MonoBehaviour
{
    #region Variables

    [SerializeField] private float rangoMaximo = 10f;
    [SerializeField] private float anguloVision = 45f;
    [SerializeField] private LayerMask layermaskObjeto;
    private LayerMask layermaskJugador;
    private GameObject jugador;

    private bool detectado;

    #endregion

    void Start()
    {
        ObtenerDatosJugador();
        detectado = false;
    }

    
    void Update()
    {
        detectado = RangoDeVision();
    }

    private bool RangoDeVision()
    {
        Vector3 direccionJugador = jugador.transform.position - transform.position;
        float anguloEnemigoJugador = Vector3.Angle(transform.forward, direccionJugador.normalized);

        if (anguloEnemigoJugador < anguloVision * 0.5f && direccionJugador.magnitude <= rangoMaximo)
        {
            RaycastHit hit;

            Debug.DrawRay(transform.position, direccionJugador.normalized, Color.blue, rangoMaximo);

            if (Physics.Raycast(transform.position, direccionJugador.normalized, rangoMaximo, layermaskJugador) &&
                !Physics.Raycast(transform.position, direccionJugador.normalized, rangoMaximo, layermaskObjeto))
            {
                return true;
            }
        }

        return false;
    }

    private void ObtenerDatosJugador()
    {
        jugador = GameObject.FindGameObjectWithTag("Player");
        layermaskJugador = jugador.layer;
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
    
}
