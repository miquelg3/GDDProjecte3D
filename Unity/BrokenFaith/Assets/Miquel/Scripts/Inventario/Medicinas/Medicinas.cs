using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Medicinas : Item
{
    public int Cantidad {  get; set; }

    public Medicinas() { }

    public Medicinas(string id, string nombre, string descripcion, float escala, int cantidad) : base(id, nombre, descripcion, escala)
    {
        Cantidad = cantidad;
    }

}
