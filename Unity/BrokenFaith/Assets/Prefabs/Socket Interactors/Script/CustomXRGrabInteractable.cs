using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CustomXRGrabInteractable : XRGrabInteractable
{
    private Transform currentHook;
    public Transform panelInventario;

    // Método para detectar colisiones con perchas
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hook"))
        {
            currentHook = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hook"))
        {
            if (currentHook == other.transform)
            {
                currentHook = null;
            }
        }
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);


        if (currentHook != null)
        {
            if (panelInventario.childCount > 0)
            {
                transform.SetParent(panelInventario.GetChild(0));
            }
        }
    }
}
