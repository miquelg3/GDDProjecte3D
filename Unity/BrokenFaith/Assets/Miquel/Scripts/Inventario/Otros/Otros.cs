using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Otros : Item
{
    public Otros() { }

    public Otros(string id, string nombre, string descripcion, float escala) : base(id, nombre, descripcion, escala)
    {
    }

}
