using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertaPuzzle2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {
        Puzzle2.PasarPuzzle2 += DesbloquearPuerta;
    }
    private void OnDisable()
    {
        Puzzle2.PasarPuzzle2 -= DesbloquearPuerta;
    }
    private void DesbloquearPuerta(bool completado)
    {
        GetComponent<Puertas>().enabled = true;
    }
}
