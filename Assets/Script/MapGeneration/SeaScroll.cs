using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaScroll : MonoBehaviour
{
    // Velocidad de desplazamiento en los ejes X e Y
    public float scrollSpeedX = 0.1f;
    public float scrollSpeedY = 0.0f;
    private float initialX;
    private float initialY;

    // Referencia al Transform del Tilemap
    private Transform tilemapTransform;

    void Start()
    {
        // Obtenemos el Transform del Tilemap
        tilemapTransform = GetComponent<Transform>();
        initialX = tilemapTransform.position.x;
        initialY = tilemapTransform.position.y;
    }

    void Update()
    {
        // Calculamos el nuevo desplazamiento en base al tiempo
        float offsetX = scrollSpeedX * Time.deltaTime;
        float offsetY = scrollSpeedY * Time.deltaTime;

        // Movemos el Tilemap aplicando el desplazamiento en los ejes
       
        if (tilemapTransform.position.x > initialX + 5)
        {
            tilemapTransform.position = new Vector3(initialX, tilemapTransform.position.y, tilemapTransform.position.z);
        }
        
        if (tilemapTransform.position.y > initialY + 3)
        {
            tilemapTransform.position = new Vector3(tilemapTransform.position.x, initialY, tilemapTransform.position.z);
        }
        tilemapTransform.position += new Vector3(offsetX, offsetY, 0);
    }
}
