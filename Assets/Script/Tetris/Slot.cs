using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData) {
        Debug.Log("OnDropSlot");
        if (eventData.pointerDrag != null) {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            GameObject draggedObject = eventData.pointerDrag;
            draggedObject.transform.SetParent(transform);
            draggedObject.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }
    }
}
