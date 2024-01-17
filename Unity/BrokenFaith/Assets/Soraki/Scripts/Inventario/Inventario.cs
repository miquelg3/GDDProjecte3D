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
    public void AgregarItem(Item ObjetoAAnyadir)
    {
        if (ObjetoAAnyadir is ICantidad )
        {
        }
    }

    public void EliminarItem(Item ObjetoAEliminar)
    {
        throw new System.NotImplementedException();
    }

    public void MostrarInventario()
    {
        throw new System.NotImplementedException();
    }
}
