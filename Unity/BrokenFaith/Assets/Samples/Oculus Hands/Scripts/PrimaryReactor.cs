using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimaryReactor : MonoBehaviour
{

    public PrimaryButtonWatcher watcher;
    public bool IsPressed = false;
    [SerializeField]
    private GameObject _uICanvas;
    
    void Start()
    {
        watcher = FindObjectOfType<PrimaryButtonWatcher>();
        //watcher.primaryButtonPress.AddListener(OnPrimaryButtonEvent());
    }

   
    private bool OnPrimaryButtonEvent()
    {
        return false;
    }
}
