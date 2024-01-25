using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Municion : Item
{
    public int Cantidad {  get; private set; }
    public TipoMunicion TipoMunicion { get; private set; }
    public int Da�o {  get; private set; }
    public Municion(string id, string nombre, bool acumulable, int cantidad, TipoMunicion tipoMunicion, int da�o) : base(id, nombre, acumulable)
    {
        Cantidad = cantidad;
        TipoMunicion = tipoMunicion;
        Da�o = da�o;
    }
}
public enum TipoMunicion
{
    Piedra,
    Metal,
    Acero
}
