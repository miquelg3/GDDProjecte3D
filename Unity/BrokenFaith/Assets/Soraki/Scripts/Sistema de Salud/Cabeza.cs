using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering.Universal.Internal;

public class Cabeza : Salud
{
    // Vol se encargara de poder pintar el circulo negro en la pantalla como que nos estamos quedando ciegos
    public Volume vol;
    private Vignette vig;
    // Vision es la variable que controla la intensidad del viñeteado usado para que el personaje se quede ciego cuando recibe heridas en la cabeza
    public float Vision { get; private set; }
    // Añadido el constructor sin parámetros por Miquel Grau el 25/02/24
    public Cabeza() { }
    public Cabeza(int vidaActual, float Vision) : base(vidaActual)
    {
        vol = Camera.main.GetComponent<Volume>();
        this.Vision = Vision;
        vol.profile.TryGet<Vignette>(out vig);
    }
    public Cabeza(int vidaActual) : base(vidaActual)
    {
        Vision = 0.25f;
        vol = Camera.main.GetComponent<Volume>();
        //vol.profile.TryGet<Vignette>(out vig);
    }

   

    public override void Herida()
    {
        Vision = NivelSalud switch
        {
            NivelSalud.Sano => 0.25f,
            NivelSalud.Herido => 0.5f,
            NivelSalud.Roto => 0.75f,
            NivelSalud.Destruido => 1f,
            _ => 0.25f,
        };
        vig.intensity.value = Vision;


    }

    public override void Infeccion()
    {
        Vision += 0.1f;
        vig.intensity.value = Vision;
    }

    public override void Curado()
    {
        Vision -= 0.1f;
        vig.intensity.value = Vision;
    }
}

