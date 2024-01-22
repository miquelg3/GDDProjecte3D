using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flecha : MonoBehaviour
{

    private float danyo = 10f;

    [SerializeField]private Rigidbody rigidbody;

    public void Lanzar(Vector3 fuerza)
    {

        rigidbody.isKinematic = false;
        rigidbody.AddForce(fuerza, ForceMode.Impulse);
        rigidbody.AddTorque(transform.right * 5f);
        transform.SetParent(null);
    }

    public void Cargar()
    {
        Vector3 posicionOriginal = transform.position;
        transform.position = posicionOriginal + transform.forward * -0.2f;
    }

    private void OnTriggerEnter(Collider objeto)
    {
        if (objeto.CompareTag("Enemigo"))
            Debug.Log(objeto.name);

        rigidbody.isKinematic = true;
        transform.SetParent(objeto.transform);
        this.enabled = false;
    }

}
