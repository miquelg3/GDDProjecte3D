using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

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
