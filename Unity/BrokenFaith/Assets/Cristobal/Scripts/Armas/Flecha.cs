using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flecha : MonoBehaviour
{
    [SerializeField] private float danyo = 10f;
    [SerializeField] private float carga = -0.2f;
    [SerializeField] private float torque = 5f;


    [SerializeField]private Rigidbody rigidbody;
    private FixedJoint joint;

    public void Lanzar(Vector3 fuerza)
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.isKinematic = false;
        rigidbody.AddForce(fuerza, ForceMode.Impulse);
        rigidbody.AddTorque(transform.right * torque);
        transform.SetParent(null);
    }

    public void Cargar()
    {       
        transform.position += transform.forward * carga;
    }

    private void OnTriggerEnter(Collider objeto)
    {
        joint = gameObject.AddComponent<FixedJoint>();

        if (objeto.CompareTag("Enemigo"))
        {
            Debug.Log(objeto.name);
        }
             
        joint.connectedBody = objeto.GetComponent<Rigidbody>();
        joint.breakForce = Mathf.Infinity;
        joint.breakTorque = Mathf.Infinity;

        rigidbody.useGravity = false;
    }

}
