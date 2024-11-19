using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;

    // Velocidad de seguimiento
    public float smoothSpeed = 0.125f;

    // Ajuste opcional para desplazar la posición de la cámara respecto al objetivo
    public Vector3 offset;

    void LateUpdate()
    {
        if (player != null)
        {
            // Calcula la posición deseada
            Vector3 desiredPosition = player.position + offset;

            // Interpola suavemente hacia la posición deseada
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Actualiza la posición de la cámara
            transform.position = smoothedPosition;
        }
    }
}
