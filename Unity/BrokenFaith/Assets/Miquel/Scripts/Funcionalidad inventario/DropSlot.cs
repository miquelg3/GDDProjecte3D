using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropSlot : MonoBehaviour, IDropHandler
{

    public void OnDrop(PointerEventData eventData)
    {
        RectTransform dropRectTransform = transform as RectTransform;

        if (!RectTransformUtility.RectangleContainsScreenPoint(dropRectTransform, eventData.position, eventData.pressEventCamera))
        {
            return;
        }
        if (dropRectTransform.parent.name == "PanelInventario")
        {
            // Mover la imagen al slot
            Debug.Log("ChildCount" + dropRectTransform.childCount);
            var image = eventData.pointerDrag.GetComponent<RectTransform>();
            image.position = dropRectTransform.position;
            image.SetParent(dropRectTransform);
        }
    }
}

