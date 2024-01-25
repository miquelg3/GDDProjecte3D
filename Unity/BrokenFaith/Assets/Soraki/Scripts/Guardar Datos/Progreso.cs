using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Progreso
{
    EstadoJugador Estado;

    public void GuardarPartida(List<Salud> pj)
    {
        string ruta = Path.Combine(Application.dataPath, "Guardado.json");
        Estado = new EstadoJugador();
        ObtenerDatos(pj);
        ContenedorDeSalud contenedor = new ContenedorDeSalud
        {
            Salud = Estado
        };
        string datosJson = JsonUtility.ToJson(contenedor);
        File.WriteAllText(ruta, datosJson);
        Debug.Log(datosJson);

    }
    public ContenedorDeSalud CargarPartida()
    {
        string ruta = Path.Combine(Application.dataPath, "Guardado.json");
        if (File.Exists(ruta))
        {
            string datosGuardados = File.ReadAllText(ruta);
            ContenedorDeSalud progresoCargado = JsonUtility.FromJson<ContenedorDeSalud>(datosGuardados);
            string datosJson = JsonUtility.ToJson(progresoCargado);
            return progresoCargado;
        }
        return null;
    }
    public void ObtenerDatos(List<Salud> pj)
    {
        Estado.C = pj[0].VidaActual;
        Estado.T = pj[1].VidaActual;
        Estado.BI = pj[2].VidaActual;
        Estado.BD = pj[3].VidaActual;
        Estado.PD = pj[4].VidaActual;
        Estado.PI = pj[5].VidaActual;
    }
    public List<Salud> SacarDatos(List<Salud> pj, ContenedorDeSalud cont)
    {
        pj[0].VidaActual = cont.Salud.C;
        pj[1].VidaActual = cont.Salud.T;
        pj[2].VidaActual = cont.Salud.BI;
        pj[3].VidaActual = cont.Salud.BD;
        pj[4].VidaActual = cont.Salud.PD;
        pj[5].VidaActual = cont.Salud.PI;
        return pj;
    }

}

[Serializable]
public class ContenedorDeSalud
{
    public EstadoJugador Salud;
}
