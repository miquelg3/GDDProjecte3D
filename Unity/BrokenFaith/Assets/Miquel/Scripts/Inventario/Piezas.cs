using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piezas : Item,ICantidad
{
    public TipoPiezas TipoPiezas { get; private set; }
    public int Cantidad { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public Piezas(string id, string nombre) : base(id, nombre)
    {
    }
    public Piezas(string id, string nombre,int cantidad,TipoPiezas tipoPiezas) : base(id, nombre)
    {
        Cantidad = cantidad;
        TipoPiezas = tipoPiezas;
    }
}
public enum TipoPiezas
{
    Filo,
    Guardas,
    Empuñadura,
    Cuerda,
    Palas
}
