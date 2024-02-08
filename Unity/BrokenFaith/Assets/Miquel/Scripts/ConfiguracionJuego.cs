using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ConfiguracionJuego : MonoBehaviour
{

    public static ConfiguracionJuego instance;

    // Para MovimientoJugador.cs
    public Transform cameraTransform;
    public TextMeshProUGUI nombreObjetoTexto;
    public GameObject pausa;
    public GameObject inventarioMenu;

    // Im�genes inventario
    public Sprite espadaImg;
    public Sprite arcoImg;
    public Sprite pistaImg;



    void Awake()
    {
        instance = this;
    }

    // Para MovimientoJugador.cs
    // Descomentar si queremos que las variables sean privadas
    /*public Transform CameraTransform()
    {
        return cameraTransform;
    }

    public TextMeshProUGUI NombreObjetoTexto()
    {
        return nombreObjetoTexto;
    }

    public GameObject Pausa()
    {
        return pausa;
    }

    public GameObject InventarioMenu()
    {
        return inventarioMenu;
    }*/


    // Im�genes del inventario
    public List<Sprite> ImagenesInventario()
    {
        List<Sprite> imagenes = new List<Sprite>();
        imagenes.Add(espadaImg);
        imagenes.Add(arcoImg);
        imagenes.Add(pistaImg);
        return imagenes;
    }

}
