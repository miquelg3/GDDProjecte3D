using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombateController : MonoBehaviour
{
    public Espada espada;
    private void OnTriggerEnter(Collider enemigo)
    {
        if (enemigo.tag == "Enemigo")
        {
            Debug.Log(enemigo.name);
        }
    }

    public void RecibirDanyo()
    {

    }
}
