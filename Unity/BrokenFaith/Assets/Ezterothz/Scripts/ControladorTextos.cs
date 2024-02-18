using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ControladorTextos : MonoBehaviour
{
    [SerializeField] private TextMeshPro[] listaTextoNotas;
    [SerializeField] private LectorCSV lectorCSVNotas;
    private string[] textoNotas;
    

    public static ControladorTextos instance;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }
	private void Start()
	{
        textoNotas = lectorCSVNotas.LeerCSV();
        EnlazarNotas();
	}

    private void EnlazarNotas()
    {
        for (int i = 0; i < listaTextoNotas.Length; i++)
        {
            if (listaTextoNotas[i] != null  && textoNotas[i] != null) listaTextoNotas[i].text = textoNotas[i];

		}
    }

}
