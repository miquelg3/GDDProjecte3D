using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public abstract class Salud
{
    public List<Salud> Lista { get; set; }
    public NivelSalud NivelSalud { get; set; }
    public bool Infectado { get; private set; }
    public float VidaActual { get; private set; }
    public bool Sangrado { get; private set; }
    public bool Muerto { get; private set; }
    public float VIDA_MAX { get; private set; }
    public Salud(NivelSalud nivelSalud, bool infectado, int vidaActual, bool sangrado, int vIDA_MAX)
    {
        Lista = new List<Salud>();
        this.NivelSalud = nivelSalud;
        this.Infectado = infectado;
        VidaActual = vidaActual;
        this.Sangrado = sangrado;
        VIDA_MAX = vIDA_MAX;
    }
    public Salud(float vIDA_MAX)
    {
        Lista = new List<Salud>();
        VidaActual = vIDA_MAX;
        VIDA_MAX = vIDA_MAX;
    }
    public Salud()
    {
        Lista = new List<Salud>();
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
    public void RecibirGolpe(float danyo, Salud parte)
    {

        if ((parte is Cabeza || parte is Torso))
        {
            RecibirGolpeParteFundamental(parte, danyo);
        }
        else
        {
            RecibirGolpeParte(parte, danyo);
        }
    }

    public void RepartirGolpe(float danyo)
    {
        bool PartesOpcionales = false;
        float residual = danyo;
        int aleatorizador = Random.Range(0, Lista.Count);
        //Controlar Bucle Infinito que produce este while
        while ((Lista[aleatorizador] is Cabeza || Lista[aleatorizador] is Torso) && Lista[aleatorizador].VidaActual == 0)
        {
            aleatorizador = Random.Range(0, Lista.Count);
        }
     
            Salud Parte = Lista[aleatorizador];
            if (residual > 0)
            {
                RecibirGolpe(danyo, Parte);
                residual -= Parte.VidaActual;
                PartesOpcionales = true;

            }
        
        if (PartesOpcionales == false)
        {
            int AUno = 0;
            while (!PartesOpcionales)
            {
                foreach (var parte in Lista)
                {
                    if (parte.VidaActual > 1 && (parte is Cabeza || parte is Torso))
                    { 
                        RecibirGolpe(danyo,parte);
                    }else if (parte.VidaActual == 1 && (parte is Cabeza || parte is Torso))
                    {
                        AUno++;
                    }
                }
                if (danyo > 0 && AUno == 2)
                {
                    Muerto = true;
                    PartesOpcionales= true;
                }
                else if (danyo == 0)
                {
                    PartesOpcionales = true;
                }
            }
        }
    }
    public void RecibirGolpeParteFundamental(Salud parte,float danyo)
    {
        if (parte.VidaActual - danyo <= 0 && parte.VidaActual > 1)
        {
            danyo -= parte.VidaActual -1;
            parte.VidaActual = 1;
            RepartirGolpe(danyo);
        }
        else if (parte.VidaActual - danyo >= 1)
        {
            parte.VidaActual -= danyo;
        }
        CambiarNivelSalud(parte);
    }

    public void RecibirGolpeParte(Salud parte, float danyo)
    {
        if (parte.VidaActual - danyo <= 0 && parte.VidaActual > 0)
        {
            danyo -= parte.VidaActual;
            parte.VidaActual = 0;
            RepartirGolpe(danyo);
        }
        else if(parte.VidaActual - danyo >= 0)
        {
            parte.VidaActual -= danyo;
        }
        CambiarNivelSalud(parte);
    }
    public void RecuperarGolpeParte(Salud parte)
    {   
        parte.VidaActual = parte.VIDA_MAX;
        CambiarNivelSalud(parte);
    }
    public void RecuperarGolpesTotales()
    {
        foreach (var parte in Lista)
        {
            RecuperarGolpeParte (parte);
            CambiarNivelSalud (parte);
        }
    }
    public void AnyadirParte(Salud Parte)
    {
        if (ComprobarParte(Parte))
        {
            Lista.Add(Parte);
        }
    }
    public void EliminarParte(Salud Parte)
    {
        foreach (var parte in Lista)
        {
            if(parte == Parte)
            {
                Lista.Remove(Parte);
            }
        }
    }
    public bool ComprobarParte(Salud Parte)
    {
        foreach (var parte in Lista)
        {
            if ((parte is Cabeza && Parte is Cabeza )||(parte is Torso && Parte is Torso))
            {
                return false;
            }
        }
        if (ComprobarExtremidades(Parte))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool ComprobarExtremidades(Salud Parte)
    {
        int CuentaPiernas = 0;
        int CuentaBrazos = 0;
        foreach (var parte in Lista)
        {
           if(parte is Brazos && Parte is Brazos)
            {
                CuentaBrazos++;
            } else if(parte is Piernas && Parte is Piernas)
            {
                CuentaPiernas++;
            }
        }
        if((Parte is Piernas && CuentaPiernas == 2) || (Parte is Brazos && CuentaBrazos == 2))
        {
            return false;
        }
        else { return true; }
    }
    public void CambiarNivelSalud(Salud Parte)
    {
        float setenta = (Parte.VIDA_MAX * 70) / 100;
        float cincuenta = (Parte.VIDA_MAX * 50) / 100;
        float veinte = (Parte.VIDA_MAX *20) / 100;
        if (Parte.VidaActual < setenta && Parte.VidaActual > cincuenta)
        {
            Parte.NivelSalud = NivelSalud.Herido;
        }
        else if (Parte.VidaActual < cincuenta && Parte.VidaActual > veinte)
        {
            Parte.NivelSalud = NivelSalud.Roto;
        }
        else if (Parte.VidaActual < veinte)
        {
            Parte.NivelSalud = NivelSalud.Destruido;
        }
        else 
        {
            Parte.NivelSalud = NivelSalud.Sano;
        }
        Parte.Herida();
    }
    

}
public enum NivelSalud
{
    Sano,
    Herido,
    Roto,
    Destruido
}