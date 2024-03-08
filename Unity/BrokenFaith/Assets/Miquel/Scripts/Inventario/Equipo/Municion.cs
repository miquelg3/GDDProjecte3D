using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Municion : Equipo
{
    public int Cantidad {  get; set; }
    public TipoMunicion TipoMunicion { get; set; }
    public Municion() { }
    public Municion(string id, string nombre, string descripcion, float daño, int cantidad, TipoMunicion tipoMunicion) : base(id, nombre, descripcion, daño)
    {
        Cantidad = cantidad;
        TipoMunicion = tipoMunicion;
    }
}
public enum TipoMunicion
{
    Piedra,
    Metal,
    Acero
}
