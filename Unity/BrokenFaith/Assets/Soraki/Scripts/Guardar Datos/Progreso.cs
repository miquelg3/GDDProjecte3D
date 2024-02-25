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

// Añadidas las dos funciones por Miquel Grau el 24/02/24 para poder serialiizar el inventario
public class Partida
{
    /*[XmlArray("Salud")]
    [XmlArrayItem("Brazos", typeof(Brazos))]
    [XmlArrayItem("Cabeza", typeof(Cabeza))]
    [XmlArrayItem("Cuerpo", typeof(Cuerpo))]
    [XmlArrayItem("Piernas", typeof(Piernas))]
    [XmlArrayItem("Torso", typeof(Torso))]
    public List<Salud> Salud { get; set; }*/

    [XmlArray("Inventario")]
    [XmlArrayItem("Clave", typeof(Clave))]
    [XmlArrayItem("Equipo", typeof(Equipo))]
    [XmlArrayItem("Item", typeof(Item))]
    [XmlArrayItem("Material", typeof(Material))]
    [XmlArrayItem("Medicina", typeof(Medicina))]
    [XmlArrayItem("Municion", typeof(Municion))]
    [XmlArrayItem("Pieza", typeof(Pieza))]
    [XmlArrayItem("Pista", typeof(Pista))]
    public List<Item> Inventario { get; set; }

    [XmlElement("Position")]
    public Vector3Serializable Position { get; set; }

    public Partida()
    {
        //Salud = new List<Salud>();
        Inventario = new List<Item>();
    }
}

public class Vector3Serializable
{
    public float X { get; set; }
    public float Y { get; set; }
    public float Z { get; set; }

    public Vector3Serializable() { }

    // Constructor para convertir de Vector3 a Vector3Serializable, si es necesario
    public Vector3Serializable(Vector3 vector)
    {
        X = vector.x;
        Y = vector.y;
        Z = vector.z;
    }
}
