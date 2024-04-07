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

            Draggable draggable = eventData.pointerDrag.GetComponent<Draggable>();
            if (draggable != null && draggable.item != null)
            {
                string slotName = dropRectTransform.name;
                int newId = ExtraerId(slotName); // Necesitas implementar este método según tu esquema de nombramiento

                // Actualizar el ID del Item
                draggable.item.Id = newId.ToString();
                Debug.Log($"Item {draggable.item.Nombre} actualizado a {draggable.item.Id}");
            }
        }
    }

    int ExtraerId(string slotName)
    {
        // Extracción básica asumiendo que el nombre es "Slot (X)"
        string idPart = slotName.Substring(slotName.IndexOf('(') + 1, slotName.IndexOf(')') - slotName.IndexOf('(') - 1);
        if (int.TryParse(idPart, out int id))
        {
            return id;
        }
        return -1; // ID no válido o error
    }
}

