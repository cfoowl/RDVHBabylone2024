using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchandiseManager : MonoBehaviour
{
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


    void Start() {
        // spawnMerchandise(EMarchandiseTypes.CEREALE);
        // spawnMerchandise(EMarchandiseTypes.BOIS);
        // spawnMerchandise(EMarchandiseTypes.ARDOISE);
        // spawnMerchandise(EMarchandiseTypes.CEREALE);
    }
}
