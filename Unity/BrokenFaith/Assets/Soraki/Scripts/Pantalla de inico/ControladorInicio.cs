using System.IO;
using TMPro;
using UnityEditor;
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
    public int Cargado;
    public AudioSource Intro;
    public Slider SliderA;
    public TextMeshProUGUI TextoAudio;
    public float Volumen;
    // Añadido por Miquel Grau el 22/02/24 para que la escena no sea una específica y se pueda elegir a través de una variable
    public SceneAsset siguienteEscena;

    // Start is called before the first frame update
    void Start()
    {
        TextoVida.SetText("200"); Vida = 200;
        TextoAudio.SetText("100");Volumen = 100;
        SliderA.value = 100;
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
        Opciones.onClick.AddListener(MostrarOpciones);
        Volver.onClick.AddListener(QuitarOpciones);
        SliderV.onValueChanged.AddListener(CambiarTextoVida);
        Empezar.onClick.AddListener(CambiarEscena);
        Cargar.onClick.AddListener(CargarEscena);
        SliderA.onValueChanged.AddListener(CambiarTextoVolumen);
    }
    
    // Update is called once per frame
    void Update()
    {
       
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
        Cargado = 0;
        PlayerPrefs.SetInt("Vida",Vida);
        PlayerPrefs.SetInt("Cargar", Cargado);
        // Añadido por Miquel Grau el 22/02/24
        SceneManager.LoadScene(siguienteEscena.name);
    }
    public void CargarEscena()
    {
        Cargado = 1;
        PlayerPrefs.SetInt("Vida", Vida);
        PlayerPrefs.SetInt("Cargar", Cargado);
        SceneManager.LoadScene("Salud");
    }
    void CambiarTextoVolumen (float Actual)
    {
        float Valor = Actual/100;
        int texto = (int)Actual;
        TextoAudio.SetText($"{texto}");
        Volumen = Valor;
        CambiarVolumen();

    }

    public void CambiarVolumen()
    {
        Intro.volume = Volumen;
    }
}
