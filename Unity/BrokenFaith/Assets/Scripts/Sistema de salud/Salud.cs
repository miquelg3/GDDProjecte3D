using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Video;


// Añadido por Miquel Grau el 25/02/24
[XmlInclude(typeof(Brazos))]
[XmlInclude(typeof(Cabeza))]
[XmlInclude(typeof(Cuerpo))]
[XmlInclude(typeof(Piernas))]
[XmlInclude(typeof(Torso))]
public abstract class Salud
{
    #region variables
    // Cambiado por Miquel Grau el 24/02/24 cambiado el nombre de la variable pública Lista a ListaSalud
    public List<Salud> ListaSalud { get; set; }
    public NivelSalud NivelSalud { get; set; }
    public bool Infectado { get; private set; }
    public int VidaActual { get; set; }
    public bool Sangrado { get; private set; }
    public bool Muerto { get; private set; }
    public int VIDA_MAX { get; set; }
    private int cont = 0;
    private int partesConVida = 6;
    #endregion
    #region constructores
    public Salud(NivelSalud nivelSalud, bool infectado, int vidaActual, bool sangrado, int vIDA_MAX)
    {
        ListaSalud = new List<Salud>();
        this.NivelSalud = nivelSalud;
        this.Infectado = infectado;
        VidaActual = vidaActual;
        this.Sangrado = sangrado;
        VIDA_MAX = vIDA_MAX;
    }
    public Salud(int vIDA_MAX)
    {
        ListaSalud = new List<Salud>();
        VidaActual = vIDA_MAX;
        VIDA_MAX = vIDA_MAX;
    }
    public Salud()
    {
        ListaSalud = new List<Salud>();
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
    public void RecibirGolpe(int danyo, Salud parte)
    {
        if (ComprobarMuerto(danyo))
        {
            Muerto = true;
        }
        else
        {
            Debug.Log($"Entro RecibirGolpeParte {cont++}");
            Debug.Log($"Partes que aun tienen vida {partesConVida}");
            RecibirGolpeParte(parte, danyo);
        }
    }
    /// <summary>
    /// Si el golpe ha destrozado la parte a la que iba dirigida, se llama a este metodo para repartir el golpe en las partes sanas priorizando las extremidades
    /// </summary>
    /// <param name="danyo">El daño que no ha podido mitigar la parte</param>
    public void RepartirGolpe(int danyo)
    {
        if (ComprobarMuerto(danyo))
        {
            Muerto = true;
        }
        else if (danyo > 0)
        {
            Salud parte = Aleatorio();
            RecibirGolpe(danyo, parte);
            Debug.Log($"Vida de la parte afectada por el golpe residual de {parte} = {parte.VidaActual}");
        }
    }
    private Salud Aleatorio()
    {
        List<int> numero = new List<int>();
        for (int i = 0; i < ListaSalud.Count; i++)
        {
            if (ListaSalud[i].VidaActual > 0)
            {
                numero.Add(i);
            }
        }
        partesConVida = numero.Count;
        int aleatorizador = Random.Range(0, numero.Count);
        return ListaSalud[numero[aleatorizador]];

    }
    /// <summary>
    /// Este metodo controla el golpe recibido en las extremidades no puede ser superior de 100 ya que superaria la vida maxima y entonces llamaria al metodo repartirgolpe
    /// </summary>
    /// <param name="parte">La parte que recibe el daño</param>
    /// <param name="danyo">El daño a recibir</param>
    public void RecibirGolpeParte(Salud parte, int danyo)
    {
        Debug.Log($"Recibiendo golpe en parte fundamental {parte} con daño {danyo}");
        
            if (parte.VidaActual > danyo)
            {
                parte.VidaActual -= danyo;
            }
            else
            {
                int danyoResidual = danyo - parte.VidaActual;
                parte.VidaActual = 0;
                Debug.Log($"Parte fundamental {parte} destruida. Repartiendo daño residual {danyoResidual}");
                RepartirGolpe(danyoResidual);
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
            parte.VidaActual = parte.VIDA_MAX;
            CambiarNivelSalud(parte);
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
        Torso pecho = (Torso)ListaSalud[1];
        RecuperarGolpeParte((Torso)pecho);
        foreach (var parte in ListaSalud)
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
            ListaSalud.Add(Parte);
        }
    }
    /// <summary>
    /// Metodo que elimina una parte del List<>
    /// </summary>
    /// <param name="Parte">La parte a eliminar</param>
    public void EliminarParte(Salud Parte)
    {
        foreach (var parte in ListaSalud)
        {
            if (parte == Parte)
            {
                ListaSalud.Remove(Parte);
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
        foreach (var parte in ListaSalud)
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
        foreach (var parte in ListaSalud)
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
        int p = PlayerPrefs.GetInt("Cargar");
        /**float setenta = (Parte.VIDA_MAX * 70) / 100;
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
        else if (Parte.VidaActual < veinte || Parte.VidaActual <= 0)
        {

            Parte.NivelSalud = NivelSalud.Destruido;
        }
        else
        {
            Parte.NivelSalud = NivelSalud.Sano;
        }*/
        float porcentajeVida = (float)Parte.VidaActual / Parte.VIDA_MAX;
        if (porcentajeVida >= 0.7f)
        {
            Parte.NivelSalud = NivelSalud.Sano;
        }
        else if (porcentajeVida >= 0.5f)
        {
            Parte.NivelSalud = NivelSalud.Herido;
        }
        else if (porcentajeVida >= 0.2f)
        {
            Parte.NivelSalud = NivelSalud.Roto;
        }
        else
        {
            Parte.NivelSalud = NivelSalud.Destruido;
        }
        if (!(p.Equals(null)))
        {
            if (p == 1 && Parte is not Torso)
            {
                Parte.Herida();
            }
            else if (p == 0)
            {
                Parte.Herida();
            }
            CambiarMaximoSaludPartes();
        }
    }
    /// <summary>
    /// Metodo que actualiza la vida maxima de cada parte dependiendo el nivel de salud del pecho, multiplicando la vida maxima de cada parte por la integridad del torso
    /// </summary>
    public void CambiarMaximoSaludPartes()
    {
        Torso pecho = (Torso)ListaSalud[1];
        foreach (var parte in ListaSalud)
        {
            if (parte is not Torso)
            {
                parte.VIDA_MAX = Mathf.RoundToInt(parte.VidaActual * pecho.IntegridadCuerpo);
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
        foreach (var item in ListaSalud)
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

        VidaMaximaCargado(pj);
        ActualizarNivelSalud();
        CargarLista(pj);
        ActualizarVidaMaxima((Torso)pj[1]);
        CambiarMaximoSaludPartes();

    }
    /// <summary>
    /// Este metodo Actualiza el estado de todas las partes despues del cargado
    /// </summary>
    public void ActualizarNivelSalud()
    {
        CambiarNivelSalud(ListaSalud[0]);
        CambiarNivelSalud(ListaSalud[1]);
        CambiarNivelSalud(ListaSalud[2]);
        CambiarNivelSalud(ListaSalud[3]);
        CambiarNivelSalud(ListaSalud[4]);
        CambiarNivelSalud(ListaSalud[5]);
    }
    /// <summary>
    /// Metodo para actualizar la vida maxima despues de que el Torso haya sido curado o cargado
    /// </summary>
    /// <param name="Pecho">El torso a evaluar el estado de salud</param>
    public void ActualizarVidaMaxima(Torso Pecho)
    {
        if (Pecho.VidaActual > 200f)
        {
            ListaSalud[1].VIDA_MAX = 200;
            ListaSalud[1].VidaActual = 200;

        }

        foreach (var parte in ListaSalud)
        {
            if (parte is not Torso)
            {
                if (Pecho.NivelSalud == NivelSalud.Sano)
                {

                    if (parte is Cabeza)
                    {
                        parte.VIDA_MAX = 200;
                    }
                    else
                    {
                        parte.VIDA_MAX = 100;
                    }
                }
                if (Pecho.NivelSalud == NivelSalud.Herido)
                {
                    if (parte is Cabeza)
                    {
                        parte.VIDA_MAX = 150;
                    }
                    else
                    {
                        parte.VIDA_MAX = 75;
                    }
                }
                if (Pecho.NivelSalud == NivelSalud.Roto)
                {
                    if (parte is Cabeza)
                    {
                        parte.VIDA_MAX = 100;
                    }
                    else
                    {
                        parte.VIDA_MAX = 50;
                    }
                }
                if (Pecho.NivelSalud == NivelSalud.Destruido)
                {
                    if (parte is Cabeza)
                    {
                        parte.VIDA_MAX = 70;
                    }
                    else
                    {
                        parte.VIDA_MAX = 35;
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
            ListaSalud[i].VidaActual = pj[i].VidaActual;
        }
        VidaMaximaCargado(pj);
    }
    /// <summary>
    /// Añadirle a la lista que pasamos por parametro la salud maxima
    /// </summary>
    /// <param name="s">la lista del cargado</param>
    public void VidaMaximaCargado(List<Salud> s)
    {
        ListaSalud[0].VIDA_MAX = 200;
        ListaSalud[1].VIDA_MAX = 200;
        ListaSalud[2].VIDA_MAX = 100;
        ListaSalud[3].VIDA_MAX = 100;
        ListaSalud[4].VIDA_MAX = 100;
        ListaSalud[5].VIDA_MAX = 100;
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