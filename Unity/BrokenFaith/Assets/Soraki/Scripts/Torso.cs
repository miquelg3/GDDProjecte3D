using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torso : Salud
{
    public float IntegridadCuerpo { get; private set; }
    public Torso(NivelSalud nivelSalud, int vidaActual) : base(nivelSalud, vidaActual)
    {
    }
    public Torso(NivelSalud nivelSalud, int vidaActual, float Integridad) : base(nivelSalud, vidaActual)
    {
        this.IntegridadCuerpo = Integridad;
    }

    public override void Herida()
    {
        IntegridadCuerpo = NivelSalud switch
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
        IntegridadCuerpo -= 0.1f;
    }

    public override void Curado()
    {
        IntegridadCuerpo += 0.1f;
    }
}
