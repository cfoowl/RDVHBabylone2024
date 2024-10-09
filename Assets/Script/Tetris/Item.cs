using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public EMarchandiseTypes type;
    public int buyingPrice = 20;
    public Vector2 initialPos;
    List<GameObject> itemUnits = new List<GameObject>();
    void Start()  {
        foreach (Transform child in transform) {
            itemUnits.Add(child.gameObject);
        }
    }

    public void setInitialPos(Vector2 initialPos) {
        this.initialPos = initialPos;
    }

    public Vector2 goToInitialPos() {
        Vector2 currentPos = GetComponent<RectTransform>().anchoredPosition;
        Vector2 delta = initialPos - currentPos;
        GetComponent<RectTransform>().anchoredPosition = initialPos;
        return delta;
    }

    public void Delete() {
        GridManager.instance.storedMerchandise.Remove(this);;
        foreach(GameObject child in itemUnits) {
            Slot slotParent = child.transform.parent.GetComponent<Slot>();
            if (slotParent != null) {
                slotParent.Free();
            }
            Destroy(child.gameObject);
        }
        Destroy(this.gameObject);
    }
}
