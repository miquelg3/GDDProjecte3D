using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Puzzle1 : MonoBehaviour
{
    [SerializeField] public GameObject Puente;
    public float rangoDeteccion = 2f;
    [SerializeField] private KeyCode Interactuar;
    private bool Reparado;
    // Start is called before the first frame update
    void Start()
    {
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
        if (Vector3.Distance(transform.position, Puente.transform.position) < rangoDeteccion && Puente.activeSelf == false)
        {
            Puente.SetActive(true);
            Reparado = true;

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Puente") && !Reparado)
        {

            Puente.SetActive(false);
        }

    }
}

