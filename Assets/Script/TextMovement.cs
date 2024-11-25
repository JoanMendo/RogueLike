using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextMovement : MonoBehaviour
{
    public float lifetime = 1f;
    public float speed = 1f;
    
    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, speed * Time.deltaTime, 0);
        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
