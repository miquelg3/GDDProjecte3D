using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventarioPrueba : MonoBehaviour
{
    #region Variables

    private GameObject Cogido;
    private bool estaCojido;
    [SerializeField] float velocidadMov = 3f;


    #endregion

    void Start()
    {
        estaCojido = false;
    }

    void Update()
    {
        ProbarRaycast();
        MoverCamara();
    }

    void ProbarRaycast()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(transform.position, Vector3.forward, out RaycastHit hit))
        {
            Debug.DrawLine(ray.origin, hit.point, Color.red);

            if (hit.collider.CompareTag("Inventario"))
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (!estaCojido)
                    {
                        Cogido = hit.collider.gameObject;
                        hit.collider.gameObject.SetActive(false);
                        estaCojido = true;
                    } else if (estaCojido && hit.collider.gameObject.GetComponent<ItemDeInventario>()?.GetItem() == null)
                    {
                        Vector3 nuevaPos = hit.collider.transform.position;
                        Destroy(hit.collider.gameObject);
                        Cogido.transform.position = nuevaPos;
                        Cogido.SetActive(true);
                        estaCojido = false;
                    }
                }
                Debug.Log(hit.collider.gameObject.GetComponent<ItemDeInventario>()?.GetName());
            }
        }
    }

    void MoverCamara()
    {
        if (Input.GetKey(KeyCode.W)) transform.position += Vector3.up * velocidadMov * Time.deltaTime;

        if (Input.GetKey(KeyCode.D)) transform.position += Vector3.right * velocidadMov * Time.deltaTime;

        if (Input.GetKey(KeyCode.S)) transform.position += Vector3.down * velocidadMov * Time.deltaTime;

        if (Input.GetKey(KeyCode.A)) transform.position += Vector3.left * velocidadMov * Time.deltaTime;
    }
}
