using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Municion : Item
{
    public int Cantidad {  get; private set; }
    public TipoMunicion TipoMunicion { get; private set; }
    public int Da�o {  get; private set; }
<<<<<<< HEAD

    public Municion(string id, string nombre) : base(id, nombre)
    {
    }
    public Municion(string id, string nombre, int cantidad, TipoMunicion tipoMunicion, int da�o) : base(id, nombre)
=======
    public Municion(string id, string nombre, bool acumulable, int cantidad, TipoMunicion tipoMunicion, int da�o) : base(id, nombre, acumulable)
>>>>>>> Feature/Miquel
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
