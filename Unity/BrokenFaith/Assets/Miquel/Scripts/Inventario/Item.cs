using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;


[XmlInclude(typeof(Clave))]
[XmlInclude(typeof(Equipo))]
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
    public bool acumulable { get; set; }

    // Constructor sin parámetro para poder serializar la clase abstracta
    protected Item() { }

    protected Item(string id, string nombre, bool acumulable)
    {
        Id = id;
        Nombre = nombre;
        this.acumulable = acumulable;
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
