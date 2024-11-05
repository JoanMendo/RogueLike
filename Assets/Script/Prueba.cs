using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prueba : MonoBehaviour
{
    private Vector2 initialLocation;
    void Start()
    {
        initialLocation = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (initialLocation.x >= transform.position.x - 1)
        {
            transform.position = new Vector2(transform.position.x + 0.01f, transform.position.y);
        }
        else
        {
            
        }
    }
}
