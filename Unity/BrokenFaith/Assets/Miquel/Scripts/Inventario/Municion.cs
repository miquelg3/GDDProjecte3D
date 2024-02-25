using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Municion : Item
{
    private string v1;
    private string v2;
    private int v3;
    private TipoMunicion piedra;
    private int v4;

    public int Cantidad {  get; set; }
    public TipoMunicion TipoMunicion { get; set; }
    public int Da�o {  get; set; }
    public Municion() { }
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
