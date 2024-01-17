using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public abstract class Salud 
{
    private List<Salud> Cuerpo {  get;set; }
    public NivelSalud NivelSalud { get;set; }
    public bool Infectado { get; private set; }
    public float VidaActual { get; private set; }
    public bool Sangrado { get; private set; }
    public bool Muerto { get; private set; }
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
    protected Salud() 
    {
        Cuerpo = new List<Salud>();
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
    public void RecibirGolpe(float danyo,Salud parte)
    {
        
        if (parte.VidaActual - danyo <= 0) 
        {
            danyo -= parte.VidaActual;
            parte.VidaActual = 0;
            RepartirGolpe(danyo);
        }
        else
        {
            parte.VidaActual -= danyo;
        }
    }

    public void RepartirGolpe(float danyo)
    {
        bool PartesOpcionales = false;
        foreach (var parte in Cuerpo)
        {
            if (parte.VidaActual > 0 && !(parte is Cabeza || parte is Torso))
            {
                RecibirGolpe(danyo, parte);
                PartesOpcionales = true;
            }
        }
        if (PartesOpcionales == false)
        {
            float DanyoSobrante = 0;
            bool repetir = true;
            int AUno = 0;
            while (repetir)
            {
                foreach (var parte in Cuerpo)
                {
                    if (parte.VidaActual > 1 && (parte is Cabeza || parte is Torso))
                    {
                        if (parte.VidaActual - danyo <= 0)
                        {
                            DanyoSobrante = danyo - (parte.VidaActual - 1);
                            parte.VidaActual = 1;
                            AUno++;
                        }
                        else
                        {
                            RecibirGolpe(danyo, parte);
                        }
                    }
                }
                if(DanyoSobrante == 0)
                {
                    repetir = false;
                }
                else if(DanyoSobrante > 0 && AUno == 2)
                {
                    Muerto = true;
                    repetir = false;

                }
            }
        }
    }
}
public enum NivelSalud
{
    Sano,
    Herido,
    Roto,
    Destruido
}