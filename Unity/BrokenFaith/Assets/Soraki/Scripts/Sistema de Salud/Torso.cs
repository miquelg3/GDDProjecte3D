using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torso : Salud
{
    // La variable integridad del cuerpo controla cuanto porcentaje de vida total tendran las otras partes del cuerpo, si resulta herido cambiara la vida maxima de las otras partes
    public float IntegridadCuerpo { get; private set; }
    public Torso(int vidaActual) : base(vidaActual)
    {
        IntegridadCuerpo = 1f;
    }
    public Torso(int vidaActual, float Integridad) : base(vidaActual)
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
