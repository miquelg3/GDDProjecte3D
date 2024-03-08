using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arma : Equipo
{
    public TipoArma TipoArma { get; set; }
    public Municion Municion { get; set; }

    public Arma() { }

    public Arma(string id, string nombre, string descipcion, float da�o, TipoArma tipoArma) : base(id, nombre, descipcion, da�o)
    {
        TipoArma = tipoArma;
    }
    public Arma(string id, string nombre, string descripcion, float da�o, TipoArma tipoArma, Municion municion) : base(id, nombre, descripcion, da�o)
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
