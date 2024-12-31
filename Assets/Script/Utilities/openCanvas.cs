using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openCanvas : MonoBehaviour
{
    public GameObject canvas;
    public AudioClip buttonClickSound;

    public void OnMouseDown()
    {

        if (canvas != null && enabled)
        {
            SoundManager.instance.PlayGlobalSound(buttonClickSound);
            canvas.SetActive(true);
        }
    }

}
