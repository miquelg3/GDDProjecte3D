using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;


[XmlInclude(typeof(Inventario))]
[XmlInclude(typeof(Arma))]
[XmlInclude(typeof(Equipo))]
[XmlInclude(typeof(Municion))]
[XmlInclude(typeof(Material))]
[XmlInclude(typeof(Medicina))]
[XmlInclude(typeof(Medicinas))]
[XmlInclude(typeof(Clave))]
[XmlInclude(typeof(Pista))]
[XmlInclude(typeof(Otros))]
[XmlInclude(typeof(Pieza))]

public abstract class Item 
{
    public string Id { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public float Escala { get; set; }

    // Constructor sin parámetro para poder serializar la clase abstracta
    protected Item() { }

    protected Item(string id, string nombre, string descripcion, float escala)
    {
        Id = id;
        Nombre = nombre;
        Descripcion = descripcion;
        Escala = escala;
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        Item other = (Item)obj;
        return Id == other.Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

}
