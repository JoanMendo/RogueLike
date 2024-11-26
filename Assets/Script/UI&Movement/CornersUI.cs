using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CornersUI : MonoBehaviour
{
    public GameObject topLeftCorner;     // Esquina superior izquierda
    public GameObject topRightCorner;   // Esquina superior derecha
    public GameObject bottomLeftCorner; // Esquina inferior izquierda
    public GameObject bottomRightCorner;// Esquina inferior derecha


    private void Start()
    {
        
    }
    
    // Método para establecer el objeto objetivo
    public void SetTarget(GameObject target)
    {
        if (target.TryGetComponent<BoxCollider2D>(out BoxCollider2D boxCollider))
        {
            // Actualiza las posiciones de las esquinas
            UpdateCorners(boxCollider);
        }
    }

    private void UpdateCorners(BoxCollider2D boxCollider)
    {
        // Obtén las posiciones del collider en coordenadas del mundo
        Vector2 center = boxCollider.transform.position + (Vector3)boxCollider.offset;
        Vector2 extents = boxCollider.size / 2.0f;

        // Calcula las esquinas en base al centro y las dimensiones del collider
        Vector2 topLeft = new Vector2(center.x - extents.x, center.y + extents.y);
        Vector2 topRight = new Vector2(center.x + extents.x, center.y + extents.y);
        Vector2 bottomLeft = new Vector2(center.x - extents.x, center.y - extents.y);
        Vector2 bottomRight = new Vector2(center.x + extents.x, center.y - extents.y);

        // Asigna las posiciones a los objetos de las esquinas
        topLeftCorner.transform.position = topLeft;
        topRightCorner.transform.position = topRight;
        bottomLeftCorner.transform.position = bottomLeft;
        bottomRightCorner.transform.position = bottomRight;

        // Asegúrate de que las esquinas estén visibles
        SetCornersActive(true);
    }

    private void SetCornersActive(bool isActive)
    {
        topLeftCorner.SetActive(isActive);
        topRightCorner.SetActive(isActive);
        bottomLeftCorner.SetActive(isActive);
        bottomRightCorner.SetActive(isActive);
    }


}

