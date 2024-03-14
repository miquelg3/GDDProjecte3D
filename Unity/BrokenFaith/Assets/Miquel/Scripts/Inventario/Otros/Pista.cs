using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pista : Otros
{
    public string Contenido { get; set; }
    public Pista() { }
    public Pista(string id, string nombre, string descripcion, float escala, string contenido) : base(id, nombre, descripcion, escala)
    {
        Contenido = contenido;
    }
}
