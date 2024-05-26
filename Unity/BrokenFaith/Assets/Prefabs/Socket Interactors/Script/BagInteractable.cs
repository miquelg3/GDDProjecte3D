using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class BagInteractable : MonoBehaviour
{
    public GameObject cubo;
    public InputActionProperty leftTriggerAction;
    public InputActionProperty rightTriggerAction;
    public GameObject panelInventario;
    public Transform leftHandTransform;
    public Transform rightHandTransform;
    public Transform posicionInventarioOculto;

    private Quaternion panelRotation;
    private bool isActive;

    private void Start()
    {
        isActive = true;
        leftTriggerAction.action.Enable();
        rightTriggerAction.action.Enable();

        panelRotation = panelInventario.transform.rotation;
    }

    private void OnEnable()
    {
        leftTriggerAction.action.performed += OnLeftTriggerPressed;
        rightTriggerAction.action.performed += OnRightTriggerPressed;
    }

    private void OnDisable()
    {
        leftTriggerAction.action.performed -= OnLeftTriggerPressed;
        rightTriggerAction.action.performed -= OnRightTriggerPressed;
    }

    private void OnLeftTriggerPressed(InputAction.CallbackContext context)
    {
        HandleTriggerPressed(leftHandTransform);
    }

    private void OnRightTriggerPressed(InputAction.CallbackContext context)
    {
        HandleTriggerPressed(rightHandTransform);
    }

    private void HandleTriggerPressed(Transform handTransform)
    {
        Debug.Log("PanelInventario activeSelf before toggle: " + isActive);

        if (isActive)
        {
            MoveToHiddenPosition();
        }
        else
        {
            panelInventario.transform.SetParent(handTransform);
            if (handTransform.name == "RightHand Controller")
                panelInventario.transform.rotation = panelRotation;
            else
                panelInventario.transform.rotation = handTransform.rotation;
            panelInventario.transform.position = handTransform.position;
            isActive = true;
        }

        Debug.Log("PanelInventario active: " + panelInventario.activeSelf);
    }

    private void MoveToHiddenPosition()
    {
        panelInventario.transform.SetParent(null);
        panelInventario.transform.position = posicionInventarioOculto.position;
        panelInventario.transform.rotation = posicionInventarioOculto.rotation;
        isActive = false;
    }
}
