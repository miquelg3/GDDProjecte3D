using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipo : Item
{
    public float Daño { get; private set; }
    public TipoArma TipoArma { get; private set; }
    public Municion Municion { get; set; }

    public Equipo(string id, string nombre, TipoArma tipoArma, float daño) : base(id, nombre)
    {
        TipoArma = tipoArma;
        Daño = daño;
    }
    public Equipo(string id, string nombre, TipoArma tipoArma, Municion municion) : base(id, nombre)
    {
        TipoArma = tipoArma;
        Municion = municion;
    }
    public Equipo(string id, string nombre, TipoArma tipoArma, Municion municion, float daño) : base(id, nombre)
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
