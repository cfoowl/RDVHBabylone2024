using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour//, IDropHandler
{
    public int row;
    public int column;
    public bool isOccupied { get; private set;}
    private GameObject currentItem;
    public void Initialize(int row, int column) {
        this.row = row;
        this.column = column;
        isOccupied = false;
        currentItem = null;
    }
    public void Occupy(GameObject item)
    {
        isOccupied = true;
        currentItem = item;
    }
    public void Free()
    {
        isOccupied = false;
        currentItem = null;
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
