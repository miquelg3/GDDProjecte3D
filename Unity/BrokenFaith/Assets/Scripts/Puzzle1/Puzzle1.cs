using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Puzzle1 : MonoBehaviour
{
    [SerializeField] public GameObject Puente;
    public float rangoDeteccion = 8f;
    [SerializeField] private KeyCode Interactuar;
    private bool Reparado;
    private Vector3 PuenteCopia;
    public delegate void EventoAbrirReja();
    public static event EventoAbrirReja AbrirReja;
    private Quaternion PuenteCopiaRotacion;
    // Start is called before the first frame update
    void Start()
    {
        PuenteCopia = Puente.transform.position;
        PuenteCopiaRotacion = Puente.transform.rotation;
        Debug.Log(PuenteCopia);
        Reparado = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(Interactuar))
        {
            RepararPuente();
        }
    }

    private void RepararPuente()
    {
        Debug.Log($"Jugador {transform.position}");
        Debug.Log($"Puente {Puente.transform.position}");
        Debug.Log($"PuenteCopia {PuenteCopia}");
        if (Vector3.Distance(transform.position, Puente.transform.position) < rangoDeteccion && Puente.activeSelf == true)
        {
            
            Puente.SetActive(false);
            Reparado = false;

        }
        if (Vector3.Distance(transform.position, PuenteCopia) < rangoDeteccion && Puente.activeSelf == false)
        {
            Puente.transform.position = PuenteCopia;
            Puente.transform.rotation = PuenteCopiaRotacion;
            Puente.SetActive(true);
            Puente.GetComponent<Rigidbody>().isKinematic = true;
            Reparado = true;

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Puente") && !Reparado)
        {
           
            //Puente.SetActive(false);
            Puente.GetComponent<Rigidbody>().isKinematic = false;
            AbrirReja?.Invoke();
        }

    }
}

