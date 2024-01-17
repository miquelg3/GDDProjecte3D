using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipo : Item
{
    public float Daño { get; private set; }
    public TipoArma TipoArma { get; private set; }


    public Equipo(string id, string nombre) : base(id, nombre)
    {
    }
    public Equipo(string id, string nombre, float daño) : base(id, nombre)
    {
        this.Daño = daño;
    }
}
public enum TipoArma
{
    Espada,
    Arco,
    Carcaj
}
