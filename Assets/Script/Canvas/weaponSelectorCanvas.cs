using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class weaponSelectorCanvas : MonoBehaviour 
{
    public AudioClip buttonClickSound;
    public GameObject fireball;
    public ScriptableObject fireballSO;
    public GameObject sword;
    public ScriptableObject swordSO;

    public void closeCanva()
    {
        gameObject.SetActive(false);
        SoundManager.instance.PlayGlobalSound(buttonClickSound);
    }

    public void selectFireball()
    {
        if (GameManager.instance.player != null)
        {
            SoundManager.instance.PlayGlobalSound(buttonClickSound);
            GameManager.instance.player.GetComponent<CharacterAttack>().isFlameThrower = false;
            GameManager.instance.player.GetComponent<CharacterAttack>().Proyectile = fireball;
            GameManager.instance.player.GetComponent<CharacterAttack>().weaponSO = fireballSO;
            GameManager.instance.player.GetComponent<CharacterAttack>().proyectilesQueue.Clear();
            gameObject.SetActive(false);
        }
    }

    public void selectSword()
    {
        if (GameManager.instance.player != null)
        {
            SoundManager.instance.PlayGlobalSound(buttonClickSound);
            GameManager.instance.player.GetComponent<CharacterAttack>().isFlameThrower = false;
            GameManager.instance.player.GetComponent<CharacterAttack>().Proyectile = sword;
            GameManager.instance.player.GetComponent<CharacterAttack>().weaponSO = swordSO;
            GameManager.instance.player.GetComponent<CharacterAttack>().proyectilesQueue.Clear();
            gameObject.SetActive(false);
        }
    }
    public void selectFlameThrower()
    {
        if (GameManager.instance.player != null)
        {
            SoundManager.instance.PlayGlobalSound(buttonClickSound);
            GameManager.instance.player.GetComponent<CharacterAttack>().isFlameThrower = true;
            GameManager.instance.player.GetComponent<CharacterAttack>().proyectilesQueue.Clear();
            gameObject.SetActive(false);
        }
    }
}
