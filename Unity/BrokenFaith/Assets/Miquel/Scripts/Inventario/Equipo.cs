using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipo : Item
{
    public float Da�o { get; private set; }
    public TipoArma TipoArma { get; private set; }
    public Municion Municion { get; set; }

<<<<<<< HEAD
    public Equipo(string id, string nombre, TipoArma tipoArma, float da�o) : base(id, nombre)
=======
    public Equipo(string id, string nombre, TipoArma tipoArma, float da�o) : base(id, nombre, false)
>>>>>>> Feature/Miquel
    {
        TipoArma = tipoArma;
        Da�o = da�o;
    }
<<<<<<< HEAD
    public Equipo(string id, string nombre, TipoArma tipoArma, Municion municion) : base(id, nombre)
=======
    public Equipo(string id, string nombre, TipoArma tipoArma, Municion municion) : base(id, nombre, false)
>>>>>>> Feature/Miquel
    {
        TipoArma = tipoArma;
        Municion = municion;
    }
<<<<<<< HEAD
    public Equipo(string id, string nombre, TipoArma tipoArma, Municion municion, float da�o) : base(id, nombre)
=======
    public Equipo(string id, string nombre, TipoArma tipoArma, Municion municion, float da�o) : base(id, nombre, false)
>>>>>>> Feature/Miquel
    {
        TipoArma = tipoArma;
        Municion = municion;
        Da�o = da�o;
    }
}
public enum TipoArma
{
    Espada,
    Arco,
    Carcaj
}
