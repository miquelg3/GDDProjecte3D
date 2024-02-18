using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cuerpo : Salud
{
    public List<Salud> Partes { get; private set; }
    public Cuerpo()
    {
        Partes = new List<Salud>();
        Salud Cabeza = new Cabeza(200);
        Salud Torso = new Torso(200);
        Salud BrazoI = new Brazos(100);
        Salud BrazoD = new Brazos(100);
        Salud PiernaD = new Piernas(100);
        Salud PiernaI = new Piernas(100);
        Partes.Add(Cabeza);
        Partes.Add(Torso);
        Partes.Add(BrazoI);
        Partes.Add(BrazoD);
        Partes.Add(PiernaD);
        Partes.Add(PiernaI);
        Lista = Partes;
    }

    public override void Curado()
    {
        throw new System.NotImplementedException();
    }

    public override void Herida()
    {
        throw new System.NotImplementedException();
    }

    public override void Infeccion()
    {
        throw new System.NotImplementedException();
    }
}
