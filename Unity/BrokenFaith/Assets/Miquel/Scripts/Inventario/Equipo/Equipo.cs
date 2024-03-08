using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipo : Item
{
    public float Daño { get; set; }

    public Equipo() { }
    
    public Equipo(string id, string nombre, string descripcion, float daño) : base(id, nombre, descripcion)
    {
        Daño = daño;
    }
}
