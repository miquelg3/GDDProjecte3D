using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piernas : Salud
{
    // La variable velocidad se encarga de controlar a la velocidad que ira nuestro personaje y el valor sera cambiado cuando resulte herido
    private float Velocidad { get; set; }
    public Piernas(int vidaActual, float Velocidad) : base(vidaActual)
    {
        this.Velocidad = Velocidad;
    }
    public Piernas(int vidaActual) : base(vidaActual)
    {
        Velocidad = 5f;
    }




    public override void Herida()
    {
        Velocidad = NivelSalud switch
        {
            NivelSalud.Sano => 5f,
            NivelSalud.Herido => 3.5f,
            NivelSalud.Roto => 2f,
            NivelSalud.Destruido => 1f,
            _ => 5f,
        };
    }

    public override void Infeccion()
    {
        Velocidad -= 0.5f;
    }

    public override void Curado()
    {
        Velocidad += 0.5f;
    }
}
    
