using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml.Serialization;
using UnityEngine;

public class Progreso
{
    EstadoJugador Estado;
    // Cambiado por Miquel Grau el 24/02/24 para que se guarden las cosas adecuadas, aún hay más cosas que se podrían guardar a medida que desarrollemos el juego. Ahora se guarda en xml porque es más fácil guardar objetos seriializables
    public void GuardarPartida(List<Salud> salud, List<Item> inventario, Vector3 position)
    {
        /*string ruta = Path.Combine(Application.dataPath, "Guardado.json");
        Estado = new EstadoJugador();
        ObtenerDatos(salud);
        ContenedorDeSalud contenedor = new ContenedorDeSalud
        {
            Salud = Estado
        };
        string datosJson = JsonUtility.ToJson(contenedor);
        File.WriteAllText(ruta, datosJson);
        Debug.Log(datosJson);*/

        // Convertir HashSet<Item> a una lista compatible con XmlSerializer
        List<Item> listaInventario = inventario.ToList();


        // Preparar el objeto a serializar que incluye salud, inventario y posición
        var partida = new Partida()
        {
            //Salud = salud,
            Inventario = listaInventario,
            Position = new Vector3Serializable(position)
        };

        string ruta = Path.Combine(Application.dataPath, "Guardado.xml");

        XmlSerializer serializer = new XmlSerializer(typeof(Partida));
        using (StreamWriter writer = new StreamWriter(ruta))
        {
            serializer.Serialize(writer, partida);
        }

        Debug.Log("Guardado en " + ruta);

    }
    public Partida CargarPartida()
    {
        /*string ruta = Path.Combine(Application.dataPath, "Guardado.json");
        if (File.Exists(ruta))
        {
            string datosGuardados = File.ReadAllText(ruta);
            ContenedorDeSalud progresoCargado = JsonUtility.FromJson<ContenedorDeSalud>(datosGuardados);
            string datosJson = JsonUtility.ToJson(progresoCargado);
            return progresoCargado;
        }
        return null;*/

        string ruta = Path.Combine(Application.dataPath, "Guardado.xml");
        XmlSerializer serializer = new XmlSerializer(typeof(Partida));
        using (StreamReader reader = new StreamReader(ruta))
        {
            return (Partida)serializer.Deserialize(reader);
        }
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
