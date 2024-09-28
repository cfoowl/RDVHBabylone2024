using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private GridManager gridManager;
    DragDrop[] neighbours;
    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
        gridManager = FindObjectOfType<GridManager>();

        // Get every other part of the item
        neighbours = new DragDrop[transform.parent.childCount -1];
        int index = 0;
        foreach(Transform child in transform.parent) {
            if (child != transform) {
                DragDrop dragDropComponent = child.GetComponent<DragDrop>();
                if (dragDropComponent != null)
                {
                    neighbours[index++] = dragDropComponent;
                }
            }
        }

    }
    public void OnPointerDown(PointerEventData eventData) {
        Debug.Log("OnPointerDown");
    }

    public void OnBeginDrag(PointerEventData eventData) {
        Debug.Log("OnBeginDrag");
        foreach(DragDrop dragDrop in neighbours) {
            dragDrop.canvasGroup.alpha = .6f;
            dragDrop.transform.SetParent(canvas.transform);
            dragDrop.canvasGroup.blocksRaycasts = false;
        }
        canvasGroup.alpha = .6f;
        transform.SetParent(canvas.transform);
        canvasGroup.blocksRaycasts = false;
    }
    public void OnDrag(PointerEventData eventData) {
        Debug.Log("OnDrag");
        foreach(DragDrop dragDrop in neighbours) {
            dragDrop.rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData) {
        Debug.Log("OnEndDrag");
        foreach(DragDrop dragDrop in neighbours) {
            dragDrop.canvasGroup.alpha = 1f;
            dragDrop.canvasGroup.blocksRaycasts = true;
        }
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        Debug.Log(gridManager.GetCell(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y).ToString());

   }

    public void OnDrop(PointerEventData eventData) {
        Debug.Log("OnDrop");
    }
}
