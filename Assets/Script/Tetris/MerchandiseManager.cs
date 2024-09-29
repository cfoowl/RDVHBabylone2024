using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchandiseManager : MonoBehaviour
{
    public static MerchandiseManager instance;
    // int a = 0;
    public GameObject[] merchandisePrefabs;
    Vector2[] spawningPos = {new Vector2(160,0), new Vector2(240, 0), new Vector2(160,-100), new Vector2(240, -100), new Vector2(160,-200), new Vector2(240, -200)};
    int merchandiseSpawned = 0;
    public void spawnMerchandise(EMarchandiseTypes type) {
        if (merchandiseSpawned < 6) {
            GameObject newMerchandise = Instantiate(merchandisePrefabs[(int)type], transform.parent);
            newMerchandise.GetComponent<RectTransform>().anchoredPosition = spawningPos[merchandiseSpawned++];
        }
        return;
    }

    public void wipeMerchandise() {
        merchandiseSpawned = 0;
        List<GameObject> remainingMerchandises = new List<GameObject>();
        foreach (Transform child in transform.parent) {
            if (child.name == "ItemUnit") {
                remainingMerchandises.Add(child.gameObject);
            }
        }
        foreach (GameObject child in remainingMerchandises) {
            child.GetComponent<DragDrop>().Delete();
        }
    }

    public void sellMerchandise(EMarchandiseTypes type) {
        foreach(Item item in GridManager.instance.storedMerchandise) {
            if (item.type == type) {
                item.Delete();
                RessourcesManager.instance.AddMoney(80);
                return;
            }
        }
    }


    void Start() {
        instance = this;
        // spawnMerchandise(EMarchandiseTypes.CEREALE);
        // spawnMerchandise(EMarchandiseTypes.BOIS);
        // spawnMerchandise(EMarchandiseTypes.ARDOISE);
        // spawnMerchandise(EMarchandiseTypes.CEREALE);
    }
    void Update() {
        // if (a++ == 500) {
        //     sellMerchandise(EMarchandiseTypes.CEREALE);
        // } else if (a < 500) {
        //     Debug.Log(a);
        // }
    }
}
