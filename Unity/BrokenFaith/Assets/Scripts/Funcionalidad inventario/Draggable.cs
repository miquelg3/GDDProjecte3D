using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Canvas canvas;
    private Vector3 startPosition;
    private Transform startParent;

    public Item item;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = gameObject.AddComponent<CanvasGroup>();
        canvas = FindObjectOfType<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
        startPosition = transform.position;
        startParent = transform.parent;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (transform.parent.parent.name == "PanelInventario")
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        else if (transform.parent.parent.name == "PanelInventarioExterno")
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor * 5;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (transform.parent == startParent)
        {
            transform.position = startPosition;
        }
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        Debug.Log(rectTransform.anchoredPosition);
    }

    public void SetItem(Item item)
    {
        Debug.Log("Item recibido");
        this.item = item;
    }
}

