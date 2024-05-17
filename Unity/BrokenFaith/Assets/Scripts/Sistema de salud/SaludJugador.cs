using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaludJugador : MonoBehaviour
{
    public Salud EstadoJugador;
    public int[] NumeroPartes = { 0, 1, 2, 3, 4, 5 };
    public float[] Probabilidades = { 0.1f, 0.1f, 0.2f, 0.2f, 0.2f,0.2f };
    // Start is called before the first frame update
    void Start()
    {
        EstadoJugador = new Cuerpo();
    }

    // Update is called once per frame
    void Update()
    {
        if(EstadoJugador.Muerto == true)
        {
            Debug.Log("CTM");
        }
    }
    private void OnEnable()
    {
        EnemigoBasico.RecibirDanyoJugador += RestarVida;
    }
    private void OnDisable()
    {
        EnemigoBasico.RecibirDanyoJugador += RestarVida;
    }

    public void RestarVida(float Danyo)
    {
        int ParteARecibirElDanyo = NumeroPonderado(NumeroPartes,Probabilidades);
        EstadoJugador.RecibirGolpe(Danyo, EstadoJugador.ListaSalud[ParteARecibirElDanyo]);
        Debug.Log(EstadoJugador.ListaSalud[ParteARecibirElDanyo].VidaActual);


    }

    int NumeroPonderado(int[] Numeros, float[] Probabilidades)
    {
        if (Numeros.Length != Probabilidades.Length)
        {
            Debug.LogError("La cantidad de números y probabilidades no coinciden.");
            return -1;
        }

        float NumeroRandom = Random.Range(0f, 1f);

        float ProbabilidadAcumulada = 0f;
        for (int i = 0; i < Probabilidades.Length; i++)
        {
            ProbabilidadAcumulada += Probabilidades[i];
            if (NumeroRandom < ProbabilidadAcumulada)
            {
                return Numeros[i];
            }
        }

        return Numeros[Numeros.Length - 1];
    }
}
