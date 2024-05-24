using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarJefe : MonoBehaviour
{
    public delegate void EventoActivarJefe(bool Activo,Transform Puerta);
    public static event EventoActivarJefe ActivarBoss;
    [SerializeField] private Transform PuertaJefe;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ActivarBoss?.Invoke(true,PuertaJefe);
        }
    }


}
