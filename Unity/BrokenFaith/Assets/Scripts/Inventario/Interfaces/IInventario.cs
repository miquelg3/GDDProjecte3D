using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventario
{
    void AgregarItem(Item Objeto);
    void EliminarItem(Item Objeto);
    void MostrarInventario();
}
