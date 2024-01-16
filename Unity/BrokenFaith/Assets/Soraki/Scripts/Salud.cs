using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public abstract class Salud 
{
    public NivelSalud NivelSalud { get;set; }
    public bool Infectado { get; private set; }
    public int VidaActual { get; private set; }
    public bool Sangrado { get; private set; }
    protected Salud(NivelSalud nivelSalud, bool infectado, int vidaActual, bool sangrado)
    {
        this.NivelSalud = nivelSalud;
        this.Infectado = infectado;
        VidaActual = vidaActual;
        this.Sangrado = sangrado;
    }
    protected Salud(NivelSalud nivelSalud, int vidaActual)
    {
        this.NivelSalud = nivelSalud;
        VidaActual = vidaActual;
    }
    public abstract void Herida();
    public void Sangrando()
    {
        this.VidaActual -= 1;
    }
    public abstract void Infeccion();
    public abstract void Curado();

}
public enum NivelSalud
{
    Sano,
    Herido,
    Roto,
    Destruido
}