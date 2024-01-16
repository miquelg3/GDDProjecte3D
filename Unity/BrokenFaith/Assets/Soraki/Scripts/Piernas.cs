using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piernas : Salud
{

    private float velocidad { get; set; }
    public Piernas(NivelSalud nivelSalud, int vidaActual, float Velocidad) : base(nivelSalud, vidaActual)
    {
        this.velocidad = Velocidad;
    }
    public Piernas(NivelSalud nivelSalud, int vidaActual) : base(nivelSalud, vidaActual)
    {
    }




    public override void Herida()
    {
        velocidad = NivelSalud switch
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
        velocidad -= 0.5f;
    }

    public override void Curado()
    {
        velocidad += 0.5f;
    }
}
    
