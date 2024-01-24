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
            return; // Si la imagen no se suelta sobre el slot, no hacer nada
        }

        // Mover la imagen al slot
        var image = eventData.pointerDrag.GetComponent<RectTransform>();
        image.position = dropRectTransform.position;
        image.SetParent(dropRectTransform); // Opcional: establecer el slot como padre para mantener la jerarquía
    }
}

