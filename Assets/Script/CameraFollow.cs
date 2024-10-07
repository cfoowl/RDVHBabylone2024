using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    public static CameraFollow instance;
    int y;
    void Start()
    {
        instance = this;
        y = 0;
    }

    // Update is called once per frame
    void Update()
    {
        y++;
        // transform.position = new Vector3(0, y, -10);
    }

    public void setGameplayCamera() {
        transform.position = new Vector3(0, 0, -10);
    }
}
