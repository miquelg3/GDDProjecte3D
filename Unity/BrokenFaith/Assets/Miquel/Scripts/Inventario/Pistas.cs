using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistas : Item
{
    public string Contenido { get; private set; }
    public Pistas(string id, string nombre) : base(id, nombre)
    {
    }
    public Pistas(string id, string nombre,string contenido) : base(id, nombre)
    {
        Contenido = contenido;
    }
}
