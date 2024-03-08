using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipo : Item
{
    public float Da�o { get; set; }

    public Equipo() { }
    
    public Equipo(string id, string nombre, string descripcion, float escala, float da�o) : base(id, nombre, descripcion, escala)
    {
        Da�o = da�o;
    }
}
