using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponSelectorCanvas : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void closeCanva()
    {
        gameObject.SetActive(false);
    }

    public void selectWeapon(int weapon, float attackSpeed)
    {
        if (GameManager.instance.player != null)
        {
            GameManager.instance.player.GetComponent<CharacterAttack>().Proyectile = GameManager.instance.weaponList[weapon];
            GameManager.instance.player.GetComponent<CharacterAttack>().AttackSpeed = attackSpeed;
            gameObject.SetActive(false);
        }
    }
}
