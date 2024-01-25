using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public abstract class Salud
{
    public List<Salud> Lista { get; set; }
    public NivelSalud NivelSalud { get; set; }
    public bool Infectado { get; private set; }
    public float VidaActual { get; set; }
    public bool Sangrado { get; private set; }
    public bool Muerto { get; private set; }
    public float VIDA_MAX { get; set; }
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
        int aleatorizador = Random.Range(0, Lista.Count);
        while (!PuedeGolpearExtremidades(aleatorizador) && HayExtremidadesSanas())
        {
            aleatorizador = Random.Range(0, Lista.Count);
        }

        Salud Parte = Lista[aleatorizador];
        if (PuedeGolpearExtremidades(aleatorizador) && HayExtremidadesSanas())
        {
            RecibirGolpe(danyo, Parte);
            PartesOpcionales = true;

        }

        if (PartesOpcionales == false)
        {
            int AUno = 0;
            while (!ComprobarMuerto(danyo))
            {
                float residual = danyo;

                if (Lista[0].VidaActual > 1)
                {
                    RecibirGolpe(danyo, Lista[0]);
                    residual -= Lista[0].VidaActual - 1;
                    if (Lista[0].VidaActual - danyo > 1)
                    {
                        break;
                    }
                }
                else AUno++;
                if (Lista[1].VidaActual > 1)
                {
                    RecibirGolpe(danyo, Lista[1]);
                    residual -= Lista[1].VidaActual - 1;
                    if (Lista[1].VidaActual - danyo > 1)
                    {
                        break;
                    }
                }
                else AUno++;

                if (AUno == 2)
                {
                    Muerto = true;
                    break;
                }
            }
        }
    }
    public void RecibirGolpeParteFundamental(Salud parte, float danyo)
    {

        if (parte.VidaActual - danyo <= 0 && parte.VidaActual > 1)
        {
            danyo -= parte.VidaActual - 1;
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
        else if (parte.VidaActual - danyo >= 0)
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
            RecuperarGolpeParte(parte);
            CambiarNivelSalud(parte);
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
            if (parte == Parte)
            {
                Lista.Remove(Parte);
            }
        }
    }
    public bool ComprobarParte(Salud Parte)
    {
        foreach (var parte in Lista)
        {
            if ((parte is Cabeza && Parte is Cabeza) || (parte is Torso && Parte is Torso))
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
            if (parte is Brazos && Parte is Brazos)
            {
                CuentaBrazos++;
            }
            else if (parte is Piernas && Parte is Piernas)
            {
                CuentaPiernas++;
            }
        }
        if ((Parte is Piernas && CuentaPiernas == 2) || (Parte is Brazos && CuentaBrazos == 2))
        {
            return false;
        }
        else { return true; }
    }
    public void CambiarNivelSalud(Salud Parte)
    {
        float setenta = (Parte.VIDA_MAX * 70) / 100;
        float cincuenta = (Parte.VIDA_MAX * 50) / 100;
        float veinte = (Parte.VIDA_MAX * 20) / 100;
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
    public bool PuedeGolpearExtremidades(int index)
    {
        if (Lista[index].VidaActual > 0 && Lista[index] is not Cabeza && Lista[index] is not Torso)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool HayExtremidadesSanas()
    {
        foreach (var Extremidad in Lista)
        {
            if (Extremidad.VidaActual > 0 && Extremidad is not Cabeza && Extremidad is not Torso)
            {
                return true;
            }
        }
        return false;
    }
    public void CambiarMaximoSaludPartes()
    {
        Torso pecho = (Torso)Lista[1];
        foreach (var parte in Lista)
        {
            if (parte is not Torso)
            {
                parte.VIDA_MAX *= pecho.IntegridadCuerpo;
                if (parte.VIDA_MAX < parte.VidaActual)
                {
                    parte.VidaActual = parte.VIDA_MAX;
                }
            }
        }
    }
    public bool ComprobarMuerto(float danyo)
    {
        float Vida = 0f;
        foreach (var item in Lista)
        {
            Vida += item.VidaActual;
        }
        if (danyo > Vida)
        {
            Muerto = true;
            return true;
        }
        else { return false; }
    }
    public void CargarEstadoPartes(List<Salud> pj)
    {
        ActualizarVidaMaxima((Torso)pj[1]);

        ActualizarNivelSalud();
        CambiarMaximoSaludPartes();
        Lista[1].Herida();
        CargarLista(pj);
    }
    public void ActualizarNivelSalud()
    {
        CambiarNivelSalud(Lista[0]);
        CambiarNivelSalud(Lista[1]);
        CambiarNivelSalud(Lista[2]);
        CambiarNivelSalud(Lista[3]);
        CambiarNivelSalud(Lista[4]);
        CambiarNivelSalud(Lista[5]);
    }
    public void ActualizarVidaMaxima(Torso Pecho)
    {
        foreach (var parte in Lista)
        {
            if (parte is not Torso)
            {
                if (Pecho.NivelSalud == NivelSalud.Sano)
                {

                    if (parte is Cabeza)
                    {
                        parte.VIDA_MAX = 200f;
                    }
                    else
                    {
                        parte.VIDA_MAX = 100f;
                    }
                }
                if(Pecho.NivelSalud == NivelSalud.Herido)
                {
                    if (parte is Cabeza)
                    {
                        parte.VIDA_MAX = 150f;
                    }
                    else
                    {
                        parte.VIDA_MAX = 75f;
                    }
                }
                if (Pecho.NivelSalud == NivelSalud.Roto)
                {
                    if (parte is Cabeza)
                    {
                        parte.VIDA_MAX = 100f;
                    }
                    else
                    {
                        parte.VIDA_MAX = 50f;
                    }
                }
                if (Pecho.NivelSalud == NivelSalud.Destruido)
                {
                    if (parte is Cabeza)
                    {
                        parte.VIDA_MAX = 70f;
                    }
                    else
                    {
                        parte.VIDA_MAX = 35f;
                    }
                }
            }
        }
    }
    public void CargarLista(List<Salud> pj)
    {
        for (int i = 0; i < 6; i++)
        {
            pj[i].VidaActual = Lista[i].VidaActual;
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