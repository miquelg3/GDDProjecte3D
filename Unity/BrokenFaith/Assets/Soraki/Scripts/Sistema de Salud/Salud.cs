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
    /// <summary>
    /// Este metodo te quita caracteristicas dependiendo del estado de la parte del cuerpo a la que este dirigido el ataque, si pasa de un estado a otro se aplicara una disminución de las caracteristicas
    /// </summary>
    public abstract void Herida();
    /// <summary>
    /// Este metodo se pone cuando este sangrando el personaje, su funcionamiento es quitar 1 de vida cada minuto a la parte que este sangrando
    /// </summary>
    public void Sangrando()
    {
        this.VidaActual -= 1;
    }
    /// <summary>
    /// Este metodo se llama cuando haya una infección, su funcionamiento consiste en quitar caracteristicas de la parte de el cuerpo en la que este activa la Infección
    /// </summary>
    public abstract void Infeccion();
    /// <summary>
    ///Este metodo restaura los valores de caracteristicas quitadas por el metodo infectado
    /// </summary>
    public abstract void Curado();

}
public enum NivelSalud
{
    Sano,
    Herido,
    Roto,
    Destruido
}