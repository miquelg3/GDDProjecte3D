using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaludJugador : MonoBehaviour
{
    public Salud EstadoJugador;
    public List<int> NumeroPartes = new List<int> {0,1,2,3,4,5};
    // Start is called before the first frame update
    void Start()
    {
        EstadoJugador = new Cuerpo();
    }

    // Update is called once per frame
    void Update()
    {
        if (EstadoJugador.Muerto == true)
        {
            Debug.Log("Has Muerto");
            GetComponent<ControlJuego>().LlamarFinPartida();
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
        Debug.Log(Danyo);
        int ParteARecibirElDanyo = NumeroPonderado();
        EstadoJugador.RecibirGolpe(Danyo, EstadoJugador.ListaSalud[ParteARecibirElDanyo]);
        CambiarVelocidad();
        Debug.Log($"Velocidad actual: {ConfiguracionJuego.instance.Velocidad}");
        Debug.Log($"Vida de {EstadoJugador.ListaSalud[ParteARecibirElDanyo]} = {EstadoJugador.ListaSalud[ParteARecibirElDanyo].VidaActual}");
    }

    int NumeroPonderado()
    {
            int NumeroRandom = Random.Range(0, NumeroPartes.Count);
            return NumeroRandom;
        
    }
    public void CambiarVelocidad() 
    {
        float velocidadActual = ConfiguracionJuego.instance.Velocidad;
        float velocidadObjetivo = 0f;
        foreach (var Partes in EstadoJugador.ListaSalud)
        {
            if(Partes is Piernas)
            {
                Piernas Pierna = (Piernas) Partes;
                velocidadObjetivo += Pierna.Velocidad;
            }
        }
        if(velocidadActual != velocidadObjetivo)
        {
            ConfiguracionJuego.instance.Velocidad = velocidadObjetivo;
        }
    }
}
