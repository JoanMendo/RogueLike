using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openCanvasOnCLick : MonoBehaviour
{
    public GameObject canvas;   
    private void OnMouseDown()
    {
        // Al hacer clic en este objeto, activa el Canvas
        if (canvas != null)
        {
            canvas.SetActive(true);

        }
    }
}
