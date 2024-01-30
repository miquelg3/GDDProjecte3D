using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PruebaVida : MonoBehaviour
{
    Salud Personaje;
    Progreso Progreso;

    // Start is called before the first frame update
    void Start()
    {
        #region Variables
        Personaje = new Cuerpo();
        Progreso = new Progreso();
        #endregion
        string ruta = Path.Combine(Application.dataPath, "Guardado.json");
        int cargar = PlayerPrefs.GetInt("Cargar");
        if (File.Exists(ruta) && cargar == 1)
        {
              Personaje.CargarEstadoPartes(Progreso.SacarDatos(Personaje.Lista,Progreso.CargarPartida()));
        }
        foreach (var Parte in Personaje.Lista)
        {
            if (Parte is Torso)
            {
                Personaje.RecibirGolpe(130f, Parte);
                Torso cabeza = (Torso) Parte;
                Debug.Log($"La vida Actual de la parte es: {Parte.VidaActual} y el nivel de salud es: {Parte.NivelSalud} y su vision es {cabeza.IntegridadCuerpo}");
            }
        }
       
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
        Progreso.GuardarPartida(Personaje.Lista);
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
        }*/
        /** Personaje.CargarEstadoPartes(Progreso.SacarDatos(Personaje.Lista,Progreso.CargarPartida()));
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
         }*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
