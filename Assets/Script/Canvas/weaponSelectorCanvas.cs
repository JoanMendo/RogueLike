using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class weaponSelectorCanvas : MonoBehaviour 
{
    public GameObject fireball;
    public ScriptableObject fireballSO;
    public GameObject sword;
    public ScriptableObject swordSO;

    public void closeCanva()
    {
        gameObject.SetActive(false);
    }

    public void selectFireball()
    {
        if (GameManager.instance.player != null)
        {
            GameManager.instance.player.GetComponent<CharacterAttack>().isFlameThrower = false;
            GameManager.instance.player.GetComponent<CharacterAttack>().Proyectile = fireball;
            GameManager.instance.player.GetComponent<CharacterAttack>().weaponSO = fireballSO;
            gameObject.SetActive(false);
        }
    }

    public void selectSword()
    {
        if (GameManager.instance.player != null)
        {
            GameManager.instance.player.GetComponent<CharacterAttack>().isFlameThrower = false;
            GameManager.instance.player.GetComponent<CharacterAttack>().Proyectile = sword;
            GameManager.instance.player.GetComponent<CharacterAttack>().weaponSO = swordSO;
            gameObject.SetActive(false);
        }
    }
    public void selectFlameThrower()
    {
        if (GameManager.instance.player != null)
        {
            GameManager.instance.player.GetComponent<CharacterAttack>().isFlameThrower = true;
            gameObject.SetActive(false);
        }
    }
}
