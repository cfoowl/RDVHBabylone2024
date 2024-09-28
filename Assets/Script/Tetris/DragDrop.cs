using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
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
    private void Start() {
        transform.SetParent(canvas.transform);
    }
    public void OnPointerDown(PointerEventData eventData) {
        Debug.Log("OnPointerDown");
    }

    public void OnBeginDrag(PointerEventData eventData) {
        Debug.Log("OnBeginDrag");
        foreach(DragDrop dragDrop in neighbours) {
            dragDrop.beginDrag(eventData);
        }
        beginDrag(eventData);
    }
    public void beginDrag(PointerEventData eventData) {
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
        if(transform.parent != canvas.transform) {
            transform.parent.GetComponent<Slot>().Free();
            transform.SetParent(canvas.transform);
        }
    }
    public void OnDrag(PointerEventData eventData) {
        Debug.Log("OnDrag");
        foreach(DragDrop dragDrop in neighbours) {
            dragDrop.drag(eventData);
        }
        drag(eventData);
    }
    public void drag(PointerEventData eventData) {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData) {
        Debug.Log("OnEndDrag");
        bool placeable = true;
        Slot[] overedSlots = new Slot[neighbours.Length + 1];

        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        
        overedSlots[0] = gridManager.GetCell(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y);
        if (overedSlots[0] == null || overedSlots[0].isOccupied == true) {
            placeable = false;
        }
        int index = 1;
        foreach(DragDrop dragDrop in neighbours) {
            dragDrop.canvasGroup.alpha = 1f;
            dragDrop.canvasGroup.blocksRaycasts = true;
            
            overedSlots[index] = gridManager.GetCell(dragDrop.rectTransform.anchoredPosition.x, dragDrop.rectTransform.anchoredPosition.y);
            if (overedSlots[index] == null || overedSlots[index].isOccupied == true) {
                placeable = false;
            }
            index++;
        }
        if (placeable) {
            overedSlots[0].Place(this);
            index = 1;
            foreach(DragDrop dragDrop in neighbours) {
                overedSlots[index++].Place(dragDrop);
            }
        }


   }
}
