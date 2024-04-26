using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

[XmlInclude(typeof(Material))]
[XmlInclude(typeof(Medicina))]

public abstract class Medicinas : Item
{
    public int Cantidad {  get; set; }

    public Medicinas() { }

    public Medicinas(string id, string nombre, string descripcion, float escala, int cantidad) : base(id, nombre, descripcion, escala)
    {
        Cantidad = cantidad;
    }

}
