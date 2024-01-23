using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PruebaVida : MonoBehaviour
{
    Salud Personaje;

    // Start is called before the first frame update
    void Start()
    {
        Personaje = new Cuerpo();
        foreach (var Parte in Personaje.Lista)
        {
           if(Parte is Cabeza)
            {
                Personaje.RecibirGolpe(200f,Parte);
                Cabeza cabeza = (Cabeza)Parte;
                Debug.Log($"La vida Actual de la parte es: {Parte.VidaActual} y el nivel de salud es: {Parte.NivelSalud} y su vision es {cabeza.vision}");
            } 
        }
        foreach (var Parte in Personaje.Lista)
        {
            if (Parte is Cabeza)
            {
                Debug.Log($"La vida Actual de la cabeza es: {Parte.VidaActual} y el nivel de salud es: {Parte.NivelSalud}");
            }else if (Parte is Brazos) 
            {
                Debug.Log($"La vida Actual de el brazo es: {Parte.VidaActual} y el nivel de salud es: {Parte.NivelSalud}");
            }else if(Parte is Torso) 
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
