using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using static UnityEditor.Progress;

public class Inventario : IInventario
{
    private HashSet<Item> Items;

    public List<Item> ItemsList { get; set; } = new List<Item>();

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
        bool resultado = Items.Remove(ObjetoAEliminar);

        if (resultado)
        {
            Debug.Log("El item fue eliminado exitosamente");
        }
        else
        {
            Debug.Log("El item no se encontró en el HashSet");
        }
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

    public void SerializarInventario(Inventario inventario)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(Inventario));
        using (TextWriter writer = new StreamWriter(@"Inventario.xml"))
        {
            serializer.Serialize(writer, inventario);
        }
    }

}
