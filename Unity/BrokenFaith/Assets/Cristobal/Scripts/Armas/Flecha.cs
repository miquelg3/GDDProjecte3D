using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flecha : MonoBehaviour
{

    private float danyo = 10f;
    

    private Rigidbody rigidbody;

    public void Lanzar(Vector3 fuerza)
    {
        rigidbody = GetComponent<Rigidbody>();

        rigidbody.isKinematic = false;
        rigidbody.AddForce(fuerza, ForceMode.Impulse);
        transform.SetParent(null);
    }

    public void Cargar()
    {
        Vector3 posicionOriginal = transform.position;
        transform.position = posicionOriginal + transform.forward * -0.2f;
    }

    private void OnTriggerEnter(Collider enemigo)
    {
        if (enemigo.tag == "Enemigo")
            Debug.Log(enemigo.name);

        
        rigidbody.isKinematic = true;
        rigidbody.velocity = Vector3.zero;
        transform.SetParent(enemigo.transform);
    }


}
