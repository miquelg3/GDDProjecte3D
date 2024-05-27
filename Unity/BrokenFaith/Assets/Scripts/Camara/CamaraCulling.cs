using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraCulling : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        EliminarCullingMask(Camera.main, "Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AgregarCullingMask(Camera camera, string layer)
    {
        int capa = LayerMask.NameToLayer(layer);
        camera.cullingMask |= 1 << capa;
    }

    public void EliminarCullingMask(Camera camera, string layer)
    {
        int capa = LayerMask.NameToLayer(layer);
        camera.cullingMask &= ~(1 << capa);
    }
}
