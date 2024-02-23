using System.Collections;
using UnityEngine;

public class PrimaryReactor : MonoBehaviour
{
    public PrimaryButtonWatcher watcher;
    public bool IsPressed = false; // used to display button state in the Unity Inspector window
    [SerializeField]
    private GameObject _uiCanvas;

    void Start()
    {
        watcher = FindObjectOfType<PrimaryButtonWatcher>();
        watcher.primaryButtonPress.AddListener(onPrimaryButtonEvent);
    }

    public void onPrimaryButtonEvent(bool pressed)
    {
        IsPressed = pressed;
        _uiCanvas.SetActive(!_uiCanvas.activeInHierarchy);
    }
}