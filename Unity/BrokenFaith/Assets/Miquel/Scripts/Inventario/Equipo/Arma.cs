using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arma : Equipo
{
    public TipoArma TipoArma { get; set; }
    public Municion Municion { get; set; }

    public Arma() { }

    public Arma(string id, string nombre, string descipcion, float daño, TipoArma tipoArma) : base(id, nombre, descipcion, daño)
    {
        TipoArma = tipoArma;
    }
    public Arma(string id, string nombre, string descripcion, float daño, TipoArma tipoArma, Municion municion) : base(id, nombre, descripcion, daño)
    {
        TipoArma = tipoArma;
        Municion = municion;
    }
}
public enum TipoArma
{
    Espada,
    Arco
}
