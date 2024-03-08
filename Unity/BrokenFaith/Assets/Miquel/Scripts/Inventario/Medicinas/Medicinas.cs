using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Medicinas : Item
{
    public int Cantidad {  get; set; }

    public Medicinas() { }

    public Medicinas(string id, string nombre, int cantidad, string descripcion) : base(id, nombre, descripcion)
    {
        Cantidad = cantidad;
    }

}
