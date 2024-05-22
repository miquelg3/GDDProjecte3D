using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piernas : Salud
{
    // La variable Vida se encarga de controlar a la Vida que ira nuestro personaje y el valor sera cambiado cuando resulte herido
    public float Velocidad { get; set; }
    // Añadido el constructor sin parámetros por Miquel Grau el 25/02/24
    public Piernas() { }
    public Piernas(int vidaActual, float Velocidad) : base(vidaActual)
    {
        this.Velocidad = Velocidad;
    }
    public Piernas(int vidaActual) : base(vidaActual)
    {
        Velocidad = 1.5f;
    }




    public override void Herida()
    {
        Velocidad = NivelSalud switch
        {
            NivelSalud.Sano => 1.5f,
            NivelSalud.Herido => 1f,
            NivelSalud.Roto => 0.8f,
            NivelSalud.Destruido => 0.5f,
            _ => 1.5f,
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
    
