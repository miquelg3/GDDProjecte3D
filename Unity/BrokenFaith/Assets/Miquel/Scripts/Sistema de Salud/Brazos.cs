using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brazos : Salud
{    // La variable fuerza sera lo que determina la fuerza de el ataque, multiplicando este por el daño del arma
    public float Fuerza { get; private set; }
    public Brazos(int vidaActual,float fuerza) : base(vidaActual)
    {
        this.Fuerza = fuerza;
    }
    public Brazos(int vidaActual) : base( vidaActual)
    {
        Fuerza = 1;
    }
    
    public override void Herida()
    {
        Fuerza = NivelSalud switch
        {
            NivelSalud.Sano => 1f,
            NivelSalud.Herido => 0.75f,
            NivelSalud.Roto => 0.5f,
            NivelSalud.Destruido => 0.35f,
            _ => 1f,
        };
    }

    public override void Infeccion()
    {
        Fuerza -= 0.15f;
    }

    public override void Curado()
    {
        Fuerza += 0.15f;
    }
}
