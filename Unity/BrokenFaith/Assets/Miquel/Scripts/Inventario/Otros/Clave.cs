using UnityEngine;

public class Clave : Otros
{
    public GameObject Triggerer {  get; private set; }

    public Clave() { }

    public Clave(string id, string nombre, string descripcion, float escala, GameObject triggerer) : base(id, nombre, descripcion, escala)
    {
        Triggerer = triggerer;
    }
}
