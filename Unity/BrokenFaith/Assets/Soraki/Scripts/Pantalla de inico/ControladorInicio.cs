using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControladorInicio : MonoBehaviour
{
    public Button Empezar;
    public Button Cargar;
    public Button Opciones;
    public Button Volver;
    public Canvas COpciones;
    public Canvas CInicio;
    public TextMeshProUGUI TextoVida;
    public Slider SliderV;
    public int Vida;
    // Start is called before the first frame update
    void Start()
    {
        TextoVida.SetText("200"); Vida = 200;
        CInicio.gameObject.SetActive(true);
        COpciones.gameObject.SetActive(false);
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
        Opciones.onClick.AddListener(MostrarOpciones);
        Volver.onClick.AddListener(QuitarOpciones);
        SliderV.onValueChanged.AddListener(CambiarTextoVida);
        Empezar.onClick.AddListener(CambiarEscena);
    }

    void MostrarOpciones()
    {
        CInicio.gameObject.SetActive(false);
        COpciones.gameObject.SetActive(true);
    }
    void QuitarOpciones()
    {
        CInicio.gameObject.SetActive(true);
        COpciones.gameObject.SetActive(false);
    }
    void CambiarTextoVida(float actual)
    {
        int valor = (int)actual;

        switch (valor)
        {
            case 0: TextoVida.SetText("200"); Vida = 200; break;
            case 1: TextoVida.SetText("400"); Vida = 400; break;
            case 2: TextoVida.SetText("600"); Vida = 600; break;
            case 3: TextoVida.SetText("800"); Vida = 800; break;
        }
    }
    public void CambiarEscena()
    {
        SceneManager.LoadScene(1);
    }

}
