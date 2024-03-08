using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class Arma : Equipo
{
    public TipoArma TipoArma { get; set; }
    public Municion Municion { get; set; }

    public Arma() { }

    public Arma(string id, string nombre, string descipcion, float escala, float da�o, TipoArma tipoArma) : base(id, nombre, descipcion, escala, da�o)
    {
        TipoArma = tipoArma;
    }
    public Arma(string id, string nombre, string descripcion, float escala, float da�o, TipoArma tipoArma, Municion municion) : base(id, nombre, descripcion, escala, da�o)
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
