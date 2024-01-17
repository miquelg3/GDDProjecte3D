using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medicinas : Item
{
    public string Texto {  get; private set; }
    public Medicinas(string id, string nombre) : base(id, nombre)
    {
    }
    public Medicinas(string id, string nombre,string texto) : base(id, nombre)
    {
        this.Texto = texto;
    }
}
public enum TipoMedicina
{
    Estado,
    Vida
}
