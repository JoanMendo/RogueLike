using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadScene : MonoBehaviour
{
    public string sceneName;
    public AudioClip buttonClickSound;
    public void OnMouseDown()
    {
        if (enabled)
        {
            SceneManager.LoadScene(sceneName);
            SoundManager.instance.PlayGlobalSound(buttonClickSound);
        }
        
    }
}

