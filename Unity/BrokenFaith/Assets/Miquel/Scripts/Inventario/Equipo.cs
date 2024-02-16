using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipo : Item
{
    public float Daño { get; private set; }
    public TipoArma TipoArma { get; private set; }
    public Municion Municion { get; set; }

<<<<<<< HEAD
    public Equipo(string id, string nombre, TipoArma tipoArma, float daño) : base(id, nombre)
=======
    public Equipo(string id, string nombre, TipoArma tipoArma, float daño) : base(id, nombre, false)
>>>>>>> Feature/Miquel
    {
        TipoArma = tipoArma;
        Daño = daño;
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
    public Equipo(string id, string nombre, TipoArma tipoArma, Municion municion, float daño) : base(id, nombre)
=======
    public Equipo(string id, string nombre, TipoArma tipoArma, Municion municion, float daño) : base(id, nombre, false)
>>>>>>> Feature/Miquel
    {
        TipoArma = tipoArma;
        Municion = municion;
        Daño = daño;
    }
}
public enum TipoArma
{
    Espada,
    Arco,
    Carcaj
}
