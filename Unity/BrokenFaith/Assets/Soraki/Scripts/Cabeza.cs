using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Cabeza : Salud
{

    public Volume vol;
    private Vignette vig;
    public Cabeza(NivelSalud nivelSalud, int vidaActual, float Vision) : base(nivelSalud, vidaActual)
    {
        this.vision = Vision;
        this.saludActual = vidaActual;
        vol.profile.TryGet<Vignette>(out vig);
    }
    public Cabeza(NivelSalud nivelSalud, int vidaActual) : base(nivelSalud, vidaActual)
    {
        this.saludActual = vidaActual;
        vol.profile.TryGet<Vignette>(out vig);
    }
    private int saludActual { get; set; }

    private float vision { get; set; }

    public override void Herida()
    {
        vision = NivelSalud switch
        {
            NivelSalud.Sano => 0.25f,
            NivelSalud.Herido => 0.5f,
            NivelSalud.Roto => 0.75f,
            NivelSalud.Destruido => 1f,
            _ => 0.25f,
        };
        vig.intensity.value = vision;


    }

    public override void Infeccion()
    {
        vision += 0.1f;
        vig.intensity.value = vision;
    }

    public override void Curado()
    {
        vision -= 0.1f;
        vig.intensity.value = vision;
    }
}

