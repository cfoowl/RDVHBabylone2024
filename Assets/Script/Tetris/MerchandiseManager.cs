using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchandiseManager : MonoBehaviour
{
    public static MerchandiseManager instance;
    // int a = 0;
    public GameObject[] merchandisePrefabs;
    private Dictionary<EMarchandiseTypes, int> merchandisePrice;
    private Dictionary<EMarchandiseTypes, string> merchandiseName;
    Vector2[] spawningPos = { new Vector2(160, 150), new Vector2(260, 150), new Vector2(160, 10), new Vector2(260, 10), new Vector2(195, -65) };
    Vector2[] rationSpawningPos = { new Vector2(170, -165), new Vector2(220, -165) };
    int merchandiseSpawned = 0;
    public void spawnMerchandise(EMarchandiseTypes type)
    {
        if (merchandiseSpawned < 5)
        {
            GameObject newMerchandise = Instantiate(merchandisePrefabs[(int)type], transform.parent);
            newMerchandise.GetComponent<RectTransform>().anchoredPosition = spawningPos[merchandiseSpawned];
            newMerchandise.GetComponent<Item>().setInitialPos(spawningPos[merchandiseSpawned]);
            merchandiseSpawned++;
        }
        return;
    }
    public void spawnRation()
    {
        int index;
        if(PortManager.instance._portEvent.bigPort) {
            index = (int)EMarchandiseTypes.BIGRATION;
        } else {
            index = (int)EMarchandiseTypes.RATION;
        }
        GameObject newRation;
        newRation = Instantiate(merchandisePrefabs[index], transform.parent);
        newRation.GetComponent<RectTransform>().anchoredPosition = rationSpawningPos[0];
        newRation.GetComponent<Item>().setInitialPos(rationSpawningPos[0]);
        newRation = Instantiate(merchandisePrefabs[index], transform.parent);
        newRation.GetComponent<RectTransform>().anchoredPosition = rationSpawningPos[1];
        newRation.GetComponent<Item>().setInitialPos(rationSpawningPos[1]);
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

    public bool sellMerchandise(EMarchandiseTypes type){
        if(deleteMerchandise(type)) {
            RessourcesManager.instance.AddMoney(merchandisePrice[type]);
            Popup.instance.catToText("- " + merchandiseName[type] + " : +" + merchandisePrice[type].ToString() + "livres \n");
            return true;
        }
        return false;
    }
    public bool deleteMerchandise(EMarchandiseTypes type)
    {
        foreach (Item item in GridManager.instance.storedMerchandise)
        {
            if (item.type == type)
            {
                item.Delete();
                return true;
            }
        }
        return false;
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

    private void createDict() {
        merchandisePrice = new Dictionary<EMarchandiseTypes, int>();
        merchandisePrice.Add(EMarchandiseTypes.CEREALE,60);
        merchandisePrice.Add(EMarchandiseTypes.CHAU, 80);
        merchandisePrice.Add(EMarchandiseTypes.VIN, 70);
        merchandisePrice.Add(EMarchandiseTypes.ARDOISE, 110);
        merchandisePrice.Add(EMarchandiseTypes.BOIS, 90);
        merchandisePrice.Add(EMarchandiseTypes.DENREE, 50);
        merchandisePrice.Add(EMarchandiseTypes.TUFFEAU, 100);
        merchandisePrice.Add(EMarchandiseTypes.SEL, 150);
        merchandisePrice.Add(EMarchandiseTypes.ANCRE, 250);

        merchandiseName = new Dictionary<EMarchandiseTypes, string>();
        merchandiseName.Add(EMarchandiseTypes.CEREALE,"céréales");
        merchandiseName.Add(EMarchandiseTypes.CHAU, "chau");
        merchandiseName.Add(EMarchandiseTypes.VIN, "futs de vin");
        merchandiseName.Add(EMarchandiseTypes.ARDOISE, "ardoises");
        merchandiseName.Add(EMarchandiseTypes.BOIS, "planches de bois");
        merchandiseName.Add(EMarchandiseTypes.DENREE, "grains de café");
        merchandiseName.Add(EMarchandiseTypes.TUFFEAU, "tuffeau");
        merchandiseName.Add(EMarchandiseTypes.SEL, "sel");
        merchandiseName.Add(EMarchandiseTypes.ANCRE, "ancre");

    }


    void Awake()
    {
        instance = this;
        createDict();
    }
}
