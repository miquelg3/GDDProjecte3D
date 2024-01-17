using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brazos : Salud
{    // La variable fuerza sera lo que determina la fuerza de el ataque, multiplicando este por el da�o del arma
    private float Fuerza { get; set; }
    public Brazos(NivelSalud nivelSalud, int vidaActual,float fuerza) : base(nivelSalud, vidaActual)
    {
        this.Fuerza = fuerza;
    }
    public Brazos(NivelSalud nivelSalud, int vidaActual) : base(nivelSalud, vidaActual)
    {
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
