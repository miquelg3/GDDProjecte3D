using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


[System.Serializable]
public class PrimaryButtonEvent : UnityEvent<bool> { }
public class PrimaryButtonWatcher : MonoBehaviour
{
    #region Variables
    public PrimaryButtonEvent primaryButtonPress;

    //private bool lastButtonState = false;
    //private List<InputDevice> devicesWithPrimaryButton;
    #endregion

    void Awake()
    {
        if(primaryButtonPress == null)
        {
            primaryButtonPress = new PrimaryButtonEvent();
        }

        //devicesWithPrimaryButton = new List<InputDevice>();
    }

    private void OnEnable()
    {
        //List<InputDevice> allDevices = new List<InputDevice>();
     }
    void Start()
    {
        
    }

   
    void Update()
    {
        
    }
}
