using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item 
{
   public string Id { get; private set; }
   public string Nombre { get; private set; }
   protected Item(string id, string nombre)
    {
        Id = id;
        Nombre = nombre;
    }
    public void AgregarItem() 
    {
    }
}
