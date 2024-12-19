using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openCanvas : MonoBehaviour
{
    public GameObject canvas;

    public void OnMouseDown()
    {

        if (canvas != null && enabled)
        {
            canvas.SetActive(true);
        }
    }

}
