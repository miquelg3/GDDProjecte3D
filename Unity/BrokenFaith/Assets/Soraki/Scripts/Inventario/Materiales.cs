using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Materiales : Item
{
    public int Cantidad { get; private set; }
    public Materiales(string id, string nombre) : base(id, nombre)
    {
    }
}
public enum TipoMateriales
{

}
