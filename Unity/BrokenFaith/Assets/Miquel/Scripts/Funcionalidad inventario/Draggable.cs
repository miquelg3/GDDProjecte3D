using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Canvas canvas;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = gameObject.AddComponent<CanvasGroup>();
        canvas = FindObjectOfType<Canvas>(); // Asegúrate de que solo haya un Canvas en la escena o referencia el correcto
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f; // Hacer el objeto más transparente mientras se arrastra
        canvasGroup.blocksRaycasts = false; // Permite que el evento de drop se registre en otros objetos
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor; // Mueve el objeto
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f; // Restaurar la transparencia
        canvasGroup.blocksRaycasts = true; // Restaurar el bloqueo de raycasts
    }
}

