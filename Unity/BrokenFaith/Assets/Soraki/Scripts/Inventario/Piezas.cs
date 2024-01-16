using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piezas : Item
{
    public int Cantidad {  get; private set; }
    public TipoPiezas TipoPiezas { get; private set; }
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
    Cuerda
}
