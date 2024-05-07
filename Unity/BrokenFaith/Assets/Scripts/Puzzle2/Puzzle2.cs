using System.Collections.Generic;
using UnityEngine;

public class Puzzle2 : MonoBehaviour
{
    #region Calaveras
    [SerializeField] private GameObject Calavera1;
    [SerializeField] private GameObject Calavera2;
    [SerializeField] private GameObject Calavera3;
    [SerializeField] private GameObject Calavera4;
    #endregion
    #region Palancas
    [SerializeField] private GameObject Palanca1;
    [SerializeField] private GameObject Palanca2;
    [SerializeField] private GameObject Palanca3;
    [SerializeField] private GameObject Palanca4;
    #endregion
    #region variables
    private GameObject[] Calaveras;
    private GameObject[] Palancas;
    private List<int> posicionCalaveras;
    private int[] OrdenCorrecto;
    private int[] OrdenJugador;
    private int contadorArray;
    [SerializeField] private GameObject Jugador;
    [SerializeField] private KeyCode Interactuar;
    private bool Completado;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        Completado = false;
        contadorArray = 0;
        Calaveras = new GameObject[4];
        posicionCalaveras = new List<int>();
        Palancas = new GameObject[4];
        OrdenCorrecto = new int[4];
        OrdenJugador = new int[4];
        AnyadirCalaverasAlArray();
        AnyadirPosicionesPosiblesALaList();
        AnyadirPalancasAlArray();
        PosicionarCalaverasAleatoriamente();
        for (int i = 0; i < OrdenCorrecto.Length; i++)
        {
            Debug.Log(OrdenCorrecto[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(Interactuar))
        {
            TirardelaPalanca();

        }
        if (contadorArray == OrdenCorrecto.Length && !Completado)
        {
            if (ComprobarOrden())
            {
                Debug.Log("Tu Mucho Listo eh????");
                Completado = true;
            }
            else
            {
                ResetearPalancas();
            }

        }

    }

    public void AnyadirCalaverasAlArray()
    {
        Calaveras[0] = Calavera1;
        Calaveras[1] = Calavera2;
        Calaveras[2] = Calavera3;
        Calaveras[3] = Calavera4;

    }
    public void AnyadirPosicionesPosiblesALaList()
    {
        posicionCalaveras.Add(0);
        posicionCalaveras.Add(-3);
        posicionCalaveras.Add(-6);
        posicionCalaveras.Add(-9);
    }
    public void AnyadirPalancasAlArray()
    {
        Palancas[0] = Palanca1;
        Palancas[1] = Palanca2;
        Palancas[2] = Palanca3;
        Palancas[3] = Palanca4;
    }
    public int SacarNumeroPosicionAleatorioCalavera()
    {
        int Aleatorio = Random.Range(0, posicionCalaveras.Count);
        int Devolver = posicionCalaveras[Aleatorio];
        posicionCalaveras.RemoveAt(Aleatorio);
        return Devolver;
    }
    public void PosicionarCalaverasAleatoriamente()
    {
        for (int i = 0; i < Calaveras.Length; i++)
        {
            int Numero = SacarNumeroPosicionAleatorioCalavera();
            switch (Numero)
            {
                case 0:
                    OrdenCorrecto[i] = 1; break;
                case -3:
                    OrdenCorrecto[i] = 2; break;
                case -6:
                    OrdenCorrecto[i] = 3; break;
                case -9:
                    OrdenCorrecto[i] = 4; break;
            }
            Calaveras[i].transform.position = new Vector3(Calaveras[i].transform.position.x + Numero, Calaveras[i].transform.position.y, Calaveras[i].transform.position.z);
        }
    }
    private void TirardelaPalanca()
    {

        Vector3 direccion = Jugador.transform.forward;
        Ray rayo = new Ray(Jugador.transform.position, direccion);
        RaycastHit hit;

        if (Physics.Raycast(rayo, out hit, 2f))
        {

            if (hit.collider.gameObject.CompareTag("Palanca"))
            {
                if (hit.collider.gameObject.transform.Find("Palanca").rotation.z < 25f)
                {
                    hit.collider.gameObject.transform.Find("Palanca").Rotate(0, 0, 50f);
                    ActualizarListaJugador(hit.collider.gameObject);
                }
               
            }

        }
       

    }
    private void ActualizarListaJugador(GameObject PalancaABuscar)
    {
        for (int i = 0; i < Palancas.Length; i++)
        {
            if (PalancaABuscar == Palancas[i])
            {
                OrdenJugador[contadorArray] = i+1;
            }
        }
        contadorArray++;
    }
    private bool ComprobarOrden() 
    {
        for(int i = 0;i<OrdenCorrecto.Length;i++) 
        {
            if (OrdenCorrecto[i] != OrdenJugador[i])
                return false;
        }
        return true;
    } 
    private void ResetearPalancas ()
    {
        for (int i = 0; i < Palancas.Length; i++)
        {
            Palancas[i].transform.Find("Palanca").Rotate(0, 0, -50f);
        }
        contadorArray=0;
    }
}

