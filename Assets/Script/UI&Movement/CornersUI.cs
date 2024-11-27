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
        SetTarget(gameObject);
    }
    
    // Método para establecer el objeto objetivo
    public void SetTarget(GameObject target)
    {
        if (target.TryGetComponent<BoxCollider2D>(out BoxCollider2D boxCollider))
        {
            // Actualiza las posiciones de las esquinas
            createCorners(boxCollider);
        }
    }

    private void createCorners(BoxCollider2D boxCollider)
    {
        GameObject cornerGroup = new GameObject("CornerGroup");
        cornerGroup.transform.SetParent(transform);
        // Obtén las posiciones del collider en coordenadas del mundo
        Vector2 center = boxCollider.transform.position + (Vector3)boxCollider.offset;
        Vector2 extents = (boxCollider.size * boxCollider.transform.localScale)/2;

        // Calcula las esquinas en base al centro y las dimensiones del collider
        Vector2 topLeft = new Vector2(center.x - extents.x, center.y + extents.y);
        Vector2 topRight = new Vector2(center.x + extents.x, center.y + extents.y);
        Vector2 bottomLeft = new Vector2(center.x - extents.x, center.y - extents.y);
        Vector2 bottomRight = new Vector2(center.x + extents.x, center.y - extents.y);

        // Asigna las posiciones a los objetos de las esquinas
        GameObject topLeftInstance = Instantiate(topLeftCorner, topLeft, Quaternion.identity, cornerGroup.transform);
        GameObject topRightInstance = Instantiate(topRightCorner, topRight, Quaternion.identity, cornerGroup.transform);
        GameObject bottomLeftInstance = Instantiate(bottomLeftCorner, bottomLeft, Quaternion.identity, cornerGroup.transform);
        GameObject bottomRightInstance= Instantiate(bottomRightCorner, bottomRight, Quaternion.identity, cornerGroup.transform);

        float scaleFactor = ( boxCollider.transform.localScale.x + boxCollider.transform.localScale.y)/2.5f;


        topLeftInstance.transform.localScale *= scaleFactor;
        topRightInstance.transform.localScale *= scaleFactor;
        bottomLeftInstance.transform.localScale *= scaleFactor;
        bottomRightInstance.transform.localScale *= scaleFactor;


        topLeftInstance.GetComponent<movementDirection>().direction = (topLeft - center).normalized;
        topRightInstance.GetComponent<movementDirection>().direction = (topRight - center).normalized;
        bottomLeftInstance.GetComponent<movementDirection>().direction = (bottomLeft - center).normalized;
        bottomRightInstance.GetComponent<movementDirection>().direction = (bottomRight - center).normalized;


    }




}

