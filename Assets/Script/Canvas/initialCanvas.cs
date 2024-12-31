using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class initialCanvas : MonoBehaviour
{
    public AudioClip buttonClickSound;
    public void exitGame()
    {
        Application.Quit();
    }

    public void startGame()
    {
        SoundManager.instance.PlayGlobalSound(buttonClickSound);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Lobby");
    }


}
