using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombateController : MonoBehaviour
{
    [SerializeField] private float velocidadPeek = 10f;
    [SerializeField] private float anguloMaximo = 20f;

    void Start()
    {
        
    }

    void Update()
    {
        Inclinarse();
    }

    private void Inclinarse()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            Quaternion inclinacion =
                Quaternion.Euler(transform.localRotation.x, transform.localRotation.y, transform.localRotation.z + anguloMaximo);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, inclinacion, Time.deltaTime * velocidadPeek);
        }

        if (Input.GetKey(KeyCode.E))
        {
            Quaternion inclinacion =
                Quaternion.Euler(transform.localRotation.x, transform.localRotation.y, transform.localRotation.z -  anguloMaximo);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, inclinacion, Time.deltaTime * velocidadPeek);
        }

    }

}
