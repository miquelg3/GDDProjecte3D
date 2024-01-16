using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipo : Item
{
    public float Da�o { get; private set; }
    public TipoArma TipoArma { get; private set; }


    public Equipo(string id, string nombre) : base(id, nombre)
    {
    }
    public Equipo(string id, string nombre, float da�o) : base(id, nombre)
    {
        this.Da�o = da�o;
    }
}
public enum TipoArma
{
    Espada,
    Arco,
    Carcaj
}
