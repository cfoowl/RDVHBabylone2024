using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour
{
    public int row;
    public int column;
    public bool isOccupied { get; private set;}
    public void Initialize(int row, int column, int scale) {
        this.row = row;
        this.column = column;
        isOccupied = false;
        GetComponent<RectTransform>().localScale = Vector3.one * (scale/100f);
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
}
