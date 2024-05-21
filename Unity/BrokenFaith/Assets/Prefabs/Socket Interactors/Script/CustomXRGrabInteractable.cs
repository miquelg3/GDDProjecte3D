using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CustomXRGrabInteractable : XRGrabInteractable
{
    private Transform currentHook;
    private bool wasGrabbed = false;

    // Método para detectar colisiones con perchas
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            wasGrabbed = true;
        }
        if (other.CompareTag("Hook") && wasGrabbed)
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
        if (wasGrabbed)
            base.OnSelectExited(args);

        if (currentHook != null)
        {
            transform.SetParent(currentHook);
        }
        
        wasGrabbed = false;
    }
}
