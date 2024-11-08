using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapScroller : MonoBehaviour
{
    // Velocidad de desplazamiento en los ejes X e Y
    public float scrollSpeedX = 0.1f;
    public float scrollSpeedY = 0.0f;

    // Referencia al Transform del Tilemap
    private Transform tilemapTransform;

    void Start()
    {
        // Obtenemos el Transform del Tilemap
        tilemapTransform = GetComponent<Transform>();
    }

    void Update()
    {
        // Calculamos el nuevo desplazamiento en base al tiempo
        float offsetX = scrollSpeedX * Time.deltaTime;
        float offsetY = scrollSpeedY * Time.deltaTime;

        // Movemos el Tilemap aplicando el desplazamiento en los ejes
       
        if (tilemapTransform.position.x > 5)
        {
            tilemapTransform.position = new Vector3(-5, tilemapTransform.position.y, tilemapTransform.position.z);
        }
        
        if (tilemapTransform.position.y > 3)
        {
            tilemapTransform.position = new Vector3(tilemapTransform.position.x, -5, tilemapTransform.position.z);
        }
        tilemapTransform.position += new Vector3(offsetX, offsetY, 0);
    }
}
