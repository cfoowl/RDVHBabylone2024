using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private RectTransform rectTransform;
    public int columns;
    public int rows;
    private Slot[,] slots;
    public GameObject slotPrefab;
    public float slotSize;
    private int slotSpacing;


    // utils
    public float xmin;
    public float ymin;
    public float xmax;
    public float ymax;
    public float width;
    public float height;

    void Start () {
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
