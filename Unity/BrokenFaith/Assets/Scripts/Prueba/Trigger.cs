using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public TextMeshPro texto;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Equals("Panel"))
        {
            texto.text = "La mano toca panel";
        }
    }
}
