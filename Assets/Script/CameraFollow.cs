using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;

    // Velocidad de seguimiento
    public float smoothSpeed = 0.125f;

    // Ajuste opcional para desplazar la posici�n de la c�mara respecto al objetivo
    public Vector3 offset;

    void LateUpdate()
    {
        if (player != null)
        {
            // Calcula la posici�n deseada
            Vector3 desiredPosition = player.position + offset;

            // Interpola suavemente hacia la posici�n deseada
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Actualiza la posici�n de la c�mara
            transform.position = smoothedPosition;
        }
    }
}
