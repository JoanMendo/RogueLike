using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;

    // Velocidad de seguimiento
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    private void OnEnable()
    {
        CharacterControler.onPlayerLoad += addPlayer;
    }

    private void OnDisable()
    {
        CharacterControler.onPlayerLoad -= addPlayer;
    }

    public void addPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void LateUpdate()
    {
        if (player != null)
        {
            // Calcula la posición deseada
            Vector3 desiredPosition = player.position + offset;

            // Interpola suavemente hacia la posición deseada
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            transform.position = smoothedPosition;
        }

    }

}
