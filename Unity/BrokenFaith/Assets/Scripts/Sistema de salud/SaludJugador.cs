using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaludJugador : MonoBehaviour
{
    public Salud EstadoJugador;
    public List<int> NumeroPartes = new List<int> {2,3,4,5};
    // Start is called before the first frame update
    void Start()
    {
        
        EstadoJugador = new Cuerpo();
        MostrarVida();
    }

    // Update is called once per frame
    void Update()
    {
        if (EstadoJugador.Muerto == true)
        {
            Debug.Log("Has Muerto");
        }
    }
    private void OnEnable()
    {
        MovimientoJugador.RecibirDanyoJugador += RestarVida;
    }
    private void OnDisable()
    {
        MovimientoJugador.RecibirDanyoJugador += RestarVida;
    }

    public void RestarVida(int Danyo)
    {

        int ParteARecibirElDanyo = NumeroPonderado();
        EstadoJugador.RecibirGolpe(Danyo, EstadoJugador.ListaSalud[ParteARecibirElDanyo]);
        MostrarVida();

    }

    int NumeroPonderado()
    {
            int NumeroRandom = Random.Range(0, NumeroPartes.Count);
            return NumeroRandom;
        
    }
    void MostrarVida()
    {
        foreach (var item in EstadoJugador.ListaSalud)
        {
            Debug.Log($"Vida de {item} = {item.VidaActual}");
            Debug.Log($"Estado de {item} = {item.NivelSalud}");
        }
    }
}
