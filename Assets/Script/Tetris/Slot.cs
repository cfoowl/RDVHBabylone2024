using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour//, IDropHandler
{
    public int row;
    public int column;
    public bool isOccupied { get; private set;}
    public void Initialize(int row, int column) {
        this.row = row;
        this.column = column;
        isOccupied = false;
    }
    public void Place(DragDrop item)
    {
        isOccupied = true;
        item.transform.SetParent(transform);
        item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

    }
    public void Free()
    {
        isOccupied = false;
    }
    // public void OnDrop(PointerEventData eventData) {
    //     Debug.Log("OnDropSlot");
    //     if (transform.childCount > 0)
    //     {
    //         Debug.Log("Slot déjà occupé !");
    //         return;
    //     }
    //     if (eventData.pointerDrag != null) {
    //         eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
    //         GameObject draggedObject = eventData.pointerDrag;
    //         draggedObject.transform.SetParent(transform);
    //         draggedObject.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    //     }
    // }
}
