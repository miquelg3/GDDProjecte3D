using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Municion : Item
{
    public int Cantidad {  get; private set; }
    public TipoMunicion TipoMunicion { get; private set; }
    public int Daño {  get; private set; }
    public Municion(string id, string nombre, bool acumulable, int cantidad, TipoMunicion tipoMunicion, int daño) : base(id, nombre, acumulable)
    {
        Cantidad = cantidad;
        TipoMunicion = tipoMunicion;
        Daño = daño;
    }
}
public enum TipoMunicion
{
    Piedra,
    Metal,
    Acero
}
