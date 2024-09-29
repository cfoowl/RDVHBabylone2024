using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchandiseManager : MonoBehaviour
{
    int a = 0;
    public GameObject[] merchandisePrefabs;
    Vector2[] spawningPos = {new Vector2(43,-25), new Vector2(110, -25), new Vector2(43, -130), new Vector2(110, -130)};
    int merchandiseSpawned = 0;
    void spawnMerchandise(EMarchandiseTypes type) {
        if (merchandiseSpawned <= 4) {
            GameObject newMerchandise = Instantiate(merchandisePrefabs[(int)type], transform.parent);
            newMerchandise.GetComponent<RectTransform>().anchoredPosition = spawningPos[merchandiseSpawned++];
        }
        return;
    }

    void wipeMerchandise() {
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

    void sellMerchandise(EMarchandiseTypes type) {
        foreach(Item item in GridManager.instance.storedMerchandise) {
            if (item.type == type) {
                item.Delete();
                return;
            }
        }
    }


    void Start() {
        spawnMerchandise(EMarchandiseTypes.CEREALE);
        spawnMerchandise(EMarchandiseTypes.BOIS);
        spawnMerchandise(EMarchandiseTypes.ARDOISE);
        spawnMerchandise(EMarchandiseTypes.CEREALE);
    }
    void Update() {
        if (a++ == 500) {
            sellMerchandise(EMarchandiseTypes.CEREALE);
        } else if (a < 500) {
            Debug.Log(a);
        }
    }
}
