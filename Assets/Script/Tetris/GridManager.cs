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
    public int slotSize;
    public int slotSpacing;


    // utils
    public float xmin;
    public float ymin;
    public float xmax;
    public float ymax;

    void Start () {
        rectTransform = GetComponent<RectTransform>();
        int width = (slotSize+slotSpacing) * columns;
        int height = (slotSize+slotSpacing) * rows;
        xmin = rectTransform.anchoredPosition.x - (width/2) + (slotSize/2);
        ymin = rectTransform.anchoredPosition.y + (height/2) - (slotSize/2);
        xmax = rectTransform.anchoredPosition.x + (width/2) + (slotSize/2);
        ymax = rectTransform.anchoredPosition.y - (height/2) - (slotSize/2);
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

                float posX = xmin + j * (slotSize + slotSpacing);
                float posY = ymin - i * (slotSize + slotSpacing);

                slotRect.anchoredPosition = new Vector2(posX, posY);

                // Stocker le slot dans la grille
                slots[i, j] = newSlot.GetComponent<Slot>();
                slots[i, j].Initialize(i, j, slotSize);
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

    void OnDrawGizmos() {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(257,257,0));
    }
}
