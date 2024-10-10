using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager instance;
    public List<Item> storedMerchandise = new List<Item>();
    private RectTransform rectTransform;
    public int columns;
    public int rows;
    private Slot[,] slots;
    public GameObject slotPrefab;
    public float slotSize;
    private int slotSpacing;


    // utils
    [HideInInspector]public float xmin;
    [HideInInspector]public float ymin;
    [HideInInspector]public float xmax;
    [HideInInspector]public float ymax;
    [HideInInspector]public float width;
    [HideInInspector]public float height;

    void Awake() {
        instance = this;
        rectTransform = GetComponent<RectTransform>();

        width = GetComponent<RectTransform>().rect.width;
        height = GetComponent<RectTransform>().rect.height;
        slotSize = (width - slotSpacing) / columns;
        xmin = GetComponent<RectTransform>().anchoredPosition.x - (width/2);
        ymin = GetComponent<RectTransform>().anchoredPosition.y - (height/2);
        xmax = GetComponent<RectTransform>().anchoredPosition.x + (width/2);
        ymax = GetComponent<RectTransform>().anchoredPosition.y + (height/2);
        CreateGrid();
    }
    void Update() {
    }
    void CreateGrid() {
        slots = new Slot[rows, columns];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                GameObject newSlot = Instantiate(slotPrefab, transform);
                RectTransform slotRect = newSlot.GetComponent<RectTransform>();

                float posX = -((width/2) - (slotSize/2)) + j * (slotSize + slotSpacing);
                float posY = (height/2) - (slotSize/2) - i * (slotSize + slotSpacing);

                slotRect.anchoredPosition = new Vector2(posX, posY);

                // Stocker le slot dans la grille
                slots[i, j] = newSlot.GetComponent<Slot>();
                slots[i, j].Initialize(i, j, slotSize);
            }
        }
    }

    public void applyInventoryDamage() {
        float damage = 0;
        foreach (Slot slot in slots) {
            if (slot.isOccupied) {
                damage += 0.1f;
            }
        }
        RessourcesManager.instance.applyDamage(damage);
    }

    public Slot GetCell(float posX, float posY) {
        // posX += slotSize/2;
        // posY += slotSize/2;
        if (posX < xmin || posX > xmax || posY < ymin || posY > ymax) {
            return null;
        }
        float relPosX = posX - xmin;
        float relPosY = -(posY - ymax);
        if (relPosX == 0f) { relPosX = 0.01f;} 
        if (relPosY == 0f) { relPosY = 0.01f;} 
        return slots[Mathf.FloorToInt(relPosY/(slotSize+slotSpacing/2)), Mathf.FloorToInt(relPosX/(slotSize+slotSpacing/2))];
    }

}
