using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public EMarchandiseTypes type;
    public int buyingPrice = 20;
    List<GameObject> itemUnits = new List<GameObject>();
    void Start()  {
        foreach (Transform child in transform) {
            itemUnits.Add(child.gameObject);
        }
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
