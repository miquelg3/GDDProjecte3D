using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

public abstract class Salud
{
    #region variables
    public List<Salud> Lista { get; set; }
    public NivelSalud NivelSalud { get; set; }
    public bool Infectado { get; private set; }
    public float VidaActual { get; set; }
    public bool Sangrado { get; private set; }
    public bool Muerto { get; private set; }

    public float VIDA_MAX { get; set; }
    #endregion
    #region constructores
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
    #endregion
    #region Metodos Abstractos
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
    #endregion
    #region Metodos
    /// <summary>
    /// En este metodo identificamos el metodo que vamos a usar para recibir el golpe dependiendo si la parte a la que le han pegado es fundamental o no luego llama al metodo correspondiente
    /// </summary>
    /// <param name="danyo">El daño que se le inflige a la parte pasada por parametro</param>
    /// <param name="parte">La parte golpeada</param>
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
    /// <summary>
    /// Si el golpe ha destrozado la parte a la que iba dirigida, se llama a este metodo para repartir el golpe en las partes sanas priorizando las extremidades
    /// </summary>
    /// <param name="danyo">El daño que no ha podido mitigar la parte</param>
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
    /// <summary>
    /// Este metodo controla el golpe recibido en la cabeza o el torso no pudiendo superar 200 ya que su vida es 200 si supera 200 se llama al metodo repartir golpe
    /// </summary>
    /// <param name="parte">La Parte que recibe el daño</param>
    /// <param name="danyo">Daño ha inflingir a dicha parte</param>
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
    /// <summary>
    /// Este metodo controla el golpe recibido en las extremidades no puede ser superior de 100 ya que superaria la vida maxima y entonces llamaria al metodo repartirgolpe
    /// </summary>
    /// <param name="parte">La parte que recibe el daño</param>
    /// <param name="danyo">El daño a recibir</param>
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
    /// <summary>
    /// Este metodo recupera la vida total de una parte y la iguala a su vida maxima
    /// </summary>
    /// <param name="parte">Parte la cual recibira la cura</param>
    public void RecuperarGolpeParte(Salud parte)
    {
        if (parte is Torso)
        {
            parte.VidaActual += parte.VIDA_MAX;
            ActualizarVidaMaxima((Torso)parte);
        }
        else
        {
            parte.VidaActual = parte.VIDA_MAX;
            CambiarNivelSalud(parte);
        }
    }
    /// <summary>
    /// Recupera los golpes totales de todas las partes, pero recupera primero la del torso para actualizar la vida maxima de dichas partes
    /// </summary>
    public void RecuperarGolpesTotales()
    {
        Torso pecho = (Torso)Lista[1];
        RecuperarGolpeParte((Torso)pecho);
        ActualizarVidaMaxima(pecho);
        foreach (var parte in Lista)
        {
            if (parte is not Torso)
            {
                RecuperarGolpeParte(parte);
                CambiarNivelSalud(parte);
            }
        }
    }
    /// <summary>
    /// Metodo que añade una parte a el List<>
    /// </summary>
    /// <param name="Parte">La parte a añadir</param>
    public void AnyadirParte(Salud Parte)
    {
        if (ComprobarParte(Parte))
        {
            Lista.Add(Parte);
        }
    }
    /// <summary>
    /// Metodo que elimina una parte del List<>
    /// </summary>
    /// <param name="Parte">La parte a eliminar</param>
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
    /// <summary>
    /// Comprobar si la parte se puede añadir ya que solo puede tener 1 cabeza y 1 torso 2 brazos y 2 piernas
    /// </summary>
    /// <param name="Parte">La parte a comprobar</param>
    /// <returns></returns>
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
    /// <summary>
    /// Comprueba que las extremidades no superen el limite de 2 de brazos o piernas, y si lo supera devuelve false
    /// </summary>
    /// <param name="Parte">Extremidad a comprobar</param>
    /// <returns></returns>
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
    /// <summary>
    /// Metodo que actualiza el estado de vida de la parte pasada por parametro dependiendo el porcentaje de vida restante
    /// </summary>
    /// <param name="Parte">Parte a cambiar el estado de salud</param>
    public void CambiarNivelSalud(Salud Parte)
    {
        float setenta = (Parte.VIDA_MAX * 70) / 100;
        float cincuenta = (Parte.VIDA_MAX * 50) / 100;
        float veinte = (Parte.VIDA_MAX * 20) / 100;
        if (Parte.VidaActual < setenta && Parte.VidaActual >= cincuenta)
        {
            Parte.NivelSalud = NivelSalud.Herido;
        }
        else if (Parte.VidaActual < cincuenta && Parte.VidaActual >= veinte)
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
    /// <summary>
    /// Este metodo es para comprobar si el numero del aleatorizador del repartir golpe ha cogido una extremidad de la lista, y que esta pueda recibir el golpe
    /// </summary>
    /// <param name="index">Numero de la parte en la lista</param>
    /// <returns></returns>
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
    /// <summary>
    /// Comprueba si se puede golpear extremidades, de lo contrario devuelve false y empezara el repartir golpe a las partes fundamentales
    /// </summary>
    /// <returns></returns>
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
    /// <summary>
    /// Metodo que actualiza la vida maxima de cada parte dependiendo el nivel de salud del pecho, multiplicando la vida maxima de cada parte por la integridad del torso
    /// </summary>
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
    /// <summary>
    /// Este metodo comprueba si el personaje ha muerto  comprobando si el daño restante es mayor a la vida de la totalidad de las partes, devuelve true si ha muerto
    /// </summary>
    /// <param name="danyo">El daño a evaluar</param>
    /// <returns></returns>
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
    /// <summary>
    /// Metodo que nos permite recuperar el estado del cuerpo a traves del fichero de guardado Json
    /// </summary>
    /// <param name="pj">La lista de partes extraidas del archivo de guardado</param>
    public void CargarEstadoPartes(List<Salud> pj)
    {
        ActualizarNivelSalud();
        ActualizarVidaMaxima((Torso)pj[1]);
        CambiarMaximoSaludPartes();
        CargarLista(pj);
    }
    /// <summary>
    /// Este metodo Actualiza el estado de todas las partes despues del cargado
    /// </summary>
    public void ActualizarNivelSalud()
    {
        CambiarNivelSalud(Lista[0]);
        CambiarNivelSalud(Lista[1]);
        CambiarNivelSalud(Lista[2]);
        CambiarNivelSalud(Lista[3]);
        CambiarNivelSalud(Lista[4]);
        CambiarNivelSalud(Lista[5]);
    }
    /// <summary>
    /// Metodo para actualizar la vida maxima despues de que el Torso haya sido curado o cargado
    /// </summary>
    /// <param name="Pecho">El torso a evaluar el estado de salud</param>
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
                if (Pecho.NivelSalud == NivelSalud.Herido)
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
    /// <summary>
    /// Metodo que sobreescribe la lista por la lista extraida del fichero de guardado
    /// </summary>
    /// <param name="pj">Lista de partes extraida del fichero Json</param>
    public void CargarLista(List<Salud> pj)
    {
        for (int i = 0; i < 6; i++)
        {
            Lista[i].VidaActual = pj[i].VidaActual;
        }
    }
    #endregion
}




public enum NivelSalud
{
    Sano,
    Herido,
    Roto,
    Destruido
}