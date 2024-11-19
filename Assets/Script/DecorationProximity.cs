using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorationProximity : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        checkNear();
    }

  

    //Methd to destroy itself if other objects with the same tag are close
 
    private void checkNear()
    {

        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, new Vector2(2f, 2f), 0);
        foreach (Collider2D col in colliders)
        {
            if (col.gameObject != this.gameObject && col.gameObject.tag == "Decoration")
            {
                Destroy(gameObject);
            }
        }
    }
}
