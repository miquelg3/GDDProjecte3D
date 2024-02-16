using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ControladorTextos : MonoBehaviour
{
    [SerializeField] private List<TextMeshPro> listaTextoNotas;
    [SerializeField] private List<string> textoNotas;

    ControladorTextos instance;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }
	private void Start()
	{
		
	}
	// Update is called once per frame
	void Update()
    {
        
    }

}
