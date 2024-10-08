using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchandiseManager : MonoBehaviour
{
    public static MerchandiseManager instance;
    // int a = 0;
    public GameObject[] merchandisePrefabs;
    Vector2[] spawningPos = { new Vector2(160, 150), new Vector2(260, 150), new Vector2(160, 10), new Vector2(260, 10), new Vector2(195, -65) };
    Vector2[] rationSpawningPos = { new Vector2(170, -165), new Vector2(220, -165) };
    int merchandiseSpawned = 0;
    public void spawnMerchandise(EMarchandiseTypes type)
    {
        if (merchandiseSpawned < 5)
        {
            GameObject newMerchandise = Instantiate(merchandisePrefabs[(int)type], transform.parent);
            newMerchandise.GetComponent<RectTransform>().anchoredPosition = spawningPos[merchandiseSpawned++];
        }
        return;
    }
    public void spawnRation()
    {
        GameObject newRation;
        newRation = Instantiate(merchandisePrefabs[(int)EMarchandiseTypes.RATION], transform.parent);
        newRation.GetComponent<RectTransform>().anchoredPosition = rationSpawningPos[0];
        newRation = Instantiate(merchandisePrefabs[(int)EMarchandiseTypes.RATION], transform.parent);
        newRation.GetComponent<RectTransform>().anchoredPosition = rationSpawningPos[1];
    }

    public void wipeMerchandise()
    {
        merchandiseSpawned = 0;
        List<GameObject> remainingMerchandises = new List<GameObject>();
        foreach (Transform child in transform.parent)
        {
            if (child.name == "ItemUnit")
            {
                remainingMerchandises.Add(child.gameObject);
            }
        }
        foreach (GameObject child in remainingMerchandises)
        {
            child.GetComponent<DragDrop>().Delete();
        }
    }

    public void sellMerchandise(EMarchandiseTypes type)
    {
        foreach (Item item in GridManager.instance.storedMerchandise)
        {
            if (item.type == type)
            {
                item.Delete();
                RessourcesManager.instance.AddMoney(60);
                return;
            }
        }
    }

    public void ConsumeFood()
    {
        foreach (Item item in GridManager.instance.storedMerchandise)
        {
            if (item.type == EMarchandiseTypes.RATION)
            {
                item.Delete();
                return;
            }
        }
    }

    public bool isInInventory(EMarchandiseTypes type)
    {
        foreach (Item item in GridManager.instance.storedMerchandise)
        {
            if (item.type == type)
            {
                return true;
            }
        }
        return false;
    }


    void Awake()
    {
        instance = this;
    }
}
