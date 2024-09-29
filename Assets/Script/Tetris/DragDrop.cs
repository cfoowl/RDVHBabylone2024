using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public GridManager gridManager;
    DragDrop[] neighbours;
    private Transform originalParent;
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
        originalParent = transform.parent;
        transform.SetParent(canvas.transform);
    }
    public void OnPointerDown(PointerEventData eventData) {
    }

    public void OnBeginDrag(PointerEventData eventData) {
        originalParent.GetComponent<CanvasGroup>().alpha = .6f;
        foreach(DragDrop dragDrop in neighbours) {
            dragDrop.beginDrag(eventData);
        }
        Slot slotParent = transform.parent.GetComponent<Slot>();
            if (slotParent != null) {
                GridManager.instance.storedMerchandise.Remove(originalParent.GetComponent<Item>());
                RessourcesManager.instance.AddMoney(20);
            }
        beginDrag(eventData);
    }
    public void beginDrag(PointerEventData eventData) {
        canvasGroup.blocksRaycasts = false;
        if(transform.parent != canvas.transform) {
            Slot slotParent = transform.parent.GetComponent<Slot>();
            if (slotParent != null) {
                slotParent.Free();
            }
            transform.SetParent(canvas.transform);
            GetComponent<Image>().color = Color.red;
        }
    }
    public void OnDrag(PointerEventData eventData) {
        foreach(DragDrop dragDrop in neighbours) {
            dragDrop.drag(eventData);
        }
        drag(eventData);
        originalParent.GetComponent<RectTransform>().anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
    public void drag(PointerEventData eventData) {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData) {
        bool placeable = true;
        Slot[] overedSlots = new Slot[neighbours.Length + 1];

        originalParent.GetComponent<CanvasGroup>().alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        
        overedSlots[0] = gridManager.GetCell(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y);
        if (overedSlots[0] == null || overedSlots[0].isOccupied == true) {
            placeable = false;
        }
        int index = 1;
        foreach(DragDrop dragDrop in neighbours) {
            dragDrop.canvasGroup.blocksRaycasts = true;
            
            overedSlots[index] = gridManager.GetCell(dragDrop.rectTransform.anchoredPosition.x, dragDrop.rectTransform.anchoredPosition.y);
            if (overedSlots[index] == null || overedSlots[index].isOccupied == true) {
                placeable = false;
            }
            index++;
        }
        if (placeable) {
            GridManager.instance.storedMerchandise.Add(originalParent.GetComponent<Item>());
            Vector2 oldPos = transform.localPosition;
            
            overedSlots[0].Place(this);
            
            Vector2 newPos = transform.parent.GetComponent<RectTransform>().anchoredPosition;
            newPos.x += (gridManager.width/2) + gridManager.xmin;
            newPos.y += - (gridManager.height/2) + gridManager.ymax;
            Vector2 delta = newPos - oldPos;

            originalParent.GetComponent<RectTransform>().anchoredPosition += delta;
            GetComponent<Image>().color = Color.green;
            index = 1;
            foreach(DragDrop dragDrop in neighbours) {
                overedSlots[index++].Place(dragDrop);
                dragDrop.GetComponent<Image>().color = Color.green;
            }
            RessourcesManager.instance.UseMoney(20);
        }

   }
   public void Delete() {
        foreach(DragDrop dragDrop in neighbours) {
            Destroy(dragDrop.gameObject);
        }
        Destroy(originalParent.gameObject);
        Destroy(gameObject);
    }
}
