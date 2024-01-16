using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brazos : Salud
{    // La variable fuerza sera lo que determina la fuerza de el ataque, multiplicando este por el daño del arma
    private float fuerza { get; set; }
    public Brazos(NivelSalud nivelSalud, int vidaActual,float fuerza) : base(nivelSalud, vidaActual)
    {
        this.fuerza = fuerza;
    }
    public Brazos(NivelSalud nivelSalud, int vidaActual) : base(nivelSalud, vidaActual)
    {
    }
     
    public override void Herida()
    {
        fuerza = NivelSalud switch
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
        fuerza -= 0.15f;
    }

    public override void Curado()
    {
        fuerza += 0.15f;
    }
}
