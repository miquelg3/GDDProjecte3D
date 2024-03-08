using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;


[XmlInclude(typeof(Clave))]
[XmlInclude(typeof(Arma))]
[XmlInclude(typeof(Inventario))]
[XmlInclude(typeof(Material))]
[XmlInclude(typeof(Medicina))]
[XmlInclude(typeof(Municion))]
[XmlInclude(typeof(Pieza))]
[XmlInclude(typeof(Pista))]

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
