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
    public int slotSize = 100;
    public int slotSpacing = 10;


    // utils
    public float xmin;
    public float ymin;
    public float xmax;
    public float ymax;

    void Start () {
        CreateGrid();
        rectTransform = GetComponent<RectTransform>();
    }
    void Update() {
        xmin = rectTransform.anchoredPosition.x;
        ymin = rectTransform.anchoredPosition.y;
        xmax = rectTransform.anchoredPosition.x + columns*(slotSize+slotSpacing);
        ymax = rectTransform.anchoredPosition.y - rows*(slotSize+slotSpacing);
    }
    void CreateGrid() {
        slots = new Slot[rows, columns];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                GameObject newSlot = Instantiate(slotPrefab, transform);
                RectTransform slotRect = newSlot.GetComponent<RectTransform>();

                float posX = j * (slotSize + slotSpacing);
                float posY = -i * (slotSize + slotSpacing);

                slotRect.anchoredPosition = new Vector2(posX, posY);

                // Stocker le slot dans la grille
                slots[i, j] = newSlot.GetComponent<Slot>();
                slots[i, j].Initialize(i, j);
            }
        }
    }

    public Slot GetCell(float posX, float posY) {
        posX += slotSize/2;
        posY -= slotSize/2;
        if (posX < xmin || posX > xmax || posY > ymin || posY < ymax) {
            return null;
        }
        float relPosX = posX - xmin;
        float relPosY = -(posY - ymin);
        return slots[Mathf.FloorToInt(relPosY/(slotSize+slotSpacing/2)), Mathf.FloorToInt(relPosX/(slotSize+slotSpacing/2))];
    }
}
