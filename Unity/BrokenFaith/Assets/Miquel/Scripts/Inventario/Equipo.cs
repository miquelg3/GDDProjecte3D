using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipo : Item
{
    public float Daño { get; set; }
    public TipoArma TipoArma { get; set; }
    public Municion Municion { get; set; }

    public Equipo() { }

    public Equipo(string id, string nombre, TipoArma tipoArma, float daño) : base(id, nombre, false)
    {
        TipoArma = tipoArma;
        Daño = daño;
    }
    public Equipo(string id, string nombre, TipoArma tipoArma, Municion municion) : base(id, nombre, false)
    {
        TipoArma = tipoArma;
        Municion = municion;
    }
    public Equipo(string id, string nombre, TipoArma tipoArma, Municion municion, float daño) : base(id, nombre, false)
    {
        TipoArma = tipoArma;
        Municion = municion;
        Daño = daño;
    }
}
public enum TipoArma
{
    Espada,
    Arco,
    Carcaj
}
