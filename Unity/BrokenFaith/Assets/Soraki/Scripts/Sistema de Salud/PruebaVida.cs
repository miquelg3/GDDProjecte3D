using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PruebaVida : MonoBehaviour
{
    Salud Personaje;
    Progreso Progreso;

    // Start is called before the first frame update
    void Start()
    {
        Personaje = new Cuerpo();
        Progreso = new Progreso();
        Progreso.GuardarPartida(Personaje.Lista);
        foreach (var Parte in Personaje.Lista)
        {
            if (Parte is Torso)
            {
                Personaje.RecibirGolpe(300f, Parte);
                Torso cabeza = (Torso) Parte;
                Personaje.CambiarMaximoSaludPartes();
                Debug.Log($"La vida Actual de la parte es: {Parte.VidaActual} y el nivel de salud es: {Parte.NivelSalud} y su vision es {cabeza.IntegridadCuerpo}");
            }
        }
       
        /**Personaje.RecuperarGolpesTotales();
        foreach (var Parte in Personaje.Lista)
        {
            if (Parte is Cabeza)
            {
                Debug.Log($"La vida Actual de la cabeza es: {Parte.VidaActual} y el nivel de salud es: {Parte.NivelSalud}");
            }
            else if (Parte is Brazos)
            {
                Debug.Log($"La vida Actual de el brazo es: {Parte.VidaActual} y el nivel de salud es: {Parte.NivelSalud}");
            }
            else if (Parte is Torso)
            {
                Debug.Log($"La vida Actual de el torso es: {Parte.VidaActual} y el nivel de salud es: {Parte.NivelSalud}");
            }
            else
            {
                Debug.Log($"La vida Actual de la pierna es: {Parte.VidaActual} y el nivel de salud es: {Parte.NivelSalud}");
            }
        }**/
        Personaje.CargarEstadoPartes(Progreso.SacarDatos(Personaje.Lista,Progreso.CargarPartida()));
        foreach (var Parte in Personaje.Lista)
        {
            if (Parte is Cabeza)
            {
                Debug.Log($"La vida Actual de la cabeza es: {Parte.VidaActual} y el nivel de salud es: {Parte.NivelSalud}");
            }
            else if (Parte is Brazos)
            {
                Debug.Log($"La vida Actual de el brazo es: {Parte.VidaActual} y el nivel de salud es: {Parte.NivelSalud}");
            }
            else if (Parte is Torso)
            {
                Debug.Log($"La vida Actual de el torso es: {Parte.VidaActual} y el nivel de salud es: {Parte.NivelSalud}");
            }
            else
            {
                Debug.Log($"La vida Actual de la pierna es: {Parte.VidaActual} y el nivel de salud es: {Parte.NivelSalud}");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
