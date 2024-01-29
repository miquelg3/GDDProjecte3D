using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ControladorInicio : MonoBehaviour
{
    public Button Empezar;
    public Button Cargar;
    public Button Opciones;
    // Start is called before the first frame update
    void Start()
    {
        string ruta = Path.Combine(Application.dataPath, "Guardado.json");
        if (File.Exists(ruta))
        {
            Cargar.interactable = true;
        }
        else
        {
            Cargar.interactable = false;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

}
