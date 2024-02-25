using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipo : Item
{
    public float Da�o { get; set; }
    public TipoArma TipoArma { get; set; }
    public Municion Municion { get; set; }

    public Equipo() { }

    public Equipo(string id, string nombre, TipoArma tipoArma, float da�o) : base(id, nombre, false)
    {
        TipoArma = tipoArma;
        Da�o = da�o;
    }
    public Equipo(string id, string nombre, TipoArma tipoArma, Municion municion) : base(id, nombre, false)
    {
        TipoArma = tipoArma;
        Municion = municion;
    }
    public Equipo(string id, string nombre, TipoArma tipoArma, Municion municion, float da�o) : base(id, nombre, false)
    {
        TipoArma = tipoArma;
        Municion = municion;
        Da�o = da�o;
    }
}
public enum TipoArma
{
    Espada,
    Arco,
    Carcaj
}
