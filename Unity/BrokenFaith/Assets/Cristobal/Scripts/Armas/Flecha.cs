using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flecha : MonoBehaviour
{
    [SerializeField] private float danyo = 1f;
    [SerializeField] private float carga = -0.2f;
    [SerializeField] private float torque = 5f;


    [SerializeField]private new Rigidbody rigidbody;
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
            objeto.GetComponent<SaludEnemigoController>().RecibirDanyo(danyo);
        } 
        else 
        {
            Debug.Log($"Colisione con algo: {objeto.name}");
            joint.connectedBody = objeto.GetComponent<Rigidbody>();
            joint.breakForce = Mathf.Infinity;
            joint.breakTorque = Mathf.Infinity;
            rigidbody.useGravity = false;
        }

    }

}
