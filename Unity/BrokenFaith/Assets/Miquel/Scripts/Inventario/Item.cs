using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item 
{
   public string Id { get; private set; }
   public string Nombre { get; private set; }
   public bool acumulable { get; private set; }
   protected Item(string id, string nombre, bool acumulable)
    {
        Id = id;
        Nombre = nombre;
        this.acumulable = acumulable;
    }
}
