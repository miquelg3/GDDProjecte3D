using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventario : IInventario
{
    private HashSet<Item> Items;
    public Inventario()
    {
        Items = new HashSet<Item>();
    }
    public void AgregarItem(Item ObjetoAAñadir)
    {
        /*if (ObjetoAAñadir is ICantidad )
        {
        }*/
        Items.Add(ObjetoAAñadir);
    }

    public void EliminarItem(Item ObjetoAEliminar)
    {
        throw new System.NotImplementedException();
    }

    public void MostrarInventario()
    {
        foreach (var item in Items)
        {
            Debug.Log($"Nombre del item: {item.Nombre}\n");
        }
        if ( Items.Count == 0 )
            Debug.Log("No hay nada en el inventario");
    }
    public HashSet<Item> GetItems() { return Items; }
}
