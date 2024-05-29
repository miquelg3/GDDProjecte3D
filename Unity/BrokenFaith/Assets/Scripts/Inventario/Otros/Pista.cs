using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pista : Otros
{
    public string Contenido { get; set; }
    public GameObject Nota { get; set; }
    public Pista() { }
    public Pista(string id, string nombre, string descripcion, float escala, string contenido, GameObject nota) : base(id, nombre, descripcion, escala)
    {
        Contenido = contenido;
        Nota = nota;
    }
}
