using UnityEngine;

public class Clave : Otros
{
    public string Triggerer {  get; set; }

    public Clave() { }

    public Clave(string id, string nombre, string descripcion, float escala, string triggerer) : base(id, nombre, descripcion, escala)
    {
        Triggerer = triggerer;
    }
}
