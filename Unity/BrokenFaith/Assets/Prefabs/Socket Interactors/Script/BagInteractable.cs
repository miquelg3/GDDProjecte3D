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

    private Quaternion panelRotation;

    private void Start()
    {
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
        panelInventario.transform.SetParent(handTransform);
        Debug.Log("PanelInventario parent name: " + panelInventario.transform.parent.name);
        if (handTransform.name == "RightHand Controller")
            panelInventario.transform.rotation = panelRotation;
        else
            panelInventario.transform.rotation = handTransform.rotation;
        panelInventario.transform.position = handTransform.position;
        Debug.Log("PanelInventario Después de la corrutina");
        bool isActive = panelInventario.activeSelf;
        Debug.Log("PanelInventario activeSelf before toggle: " + isActive);
        if (isActive)
        {
            panelInventario.SetActive(false);
        }
        else
        {
            panelInventario.SetActive(true);
        }
        Debug.Log("PanelInventario active: " + panelInventario.activeSelf);
    }
}
