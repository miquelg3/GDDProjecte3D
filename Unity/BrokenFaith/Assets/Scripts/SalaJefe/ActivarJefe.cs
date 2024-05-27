using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarJefe : MonoBehaviour
{
    public delegate void EventoActivarJefe(bool Activo);
    public static event EventoActivarJefe ActivarBoss;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ActivarBoss?.Invoke(true);
        }
    }


}
