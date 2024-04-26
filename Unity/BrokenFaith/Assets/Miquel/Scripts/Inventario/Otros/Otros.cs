using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

[XmlInclude(typeof(Clave))]
[XmlInclude(typeof(Pista))]

public abstract class Otros : Item
{
    public Otros() { }

    public Otros(string id, string nombre, string descripcion, float escala) : base(id, nombre, descripcion, escala)
    {
    }

}
