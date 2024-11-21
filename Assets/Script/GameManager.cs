using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject player;
    public List<Vector2> levelPositions;
    public GameObject currentLevel;
    public List<GameObject> enemyList = new List<GameObject>();
    public GameObject Cloud;

    private GameObject cloud;

    void Awake()
    {  
        instance = this;
    }

    public void checkList()
    {
        if (enemyList.Count == 0)
        {
            cloud = Instantiate(Cloud, new Vector3(player.transform.position.x, player.transform.position.y + 15, 0), Quaternion.identity);
            StartCoroutine(CloudMovement(player.transform.position));
        }
    }

    public IEnumerator CloudMovement(Vector2 transform)
    {
        //Que la nube se deslice hasta la posicion del jugador
        while (Vector3.Distance(cloud.transform.position,transform) > 0.1f)
        {
            cloud.transform.position = Vector3.MoveTowards(cloud.transform.position, transform, 0.1f);
            yield return new WaitForSeconds(0.01f);
        }
        
        //CloudFreezesPlayer(player);
        currentLevel.GetComponent<ProceduralTilemap>().makeNewLevel(50, 0);


    }

    public void CloudFreezesPlayer(GameObject player)
    {
        player.GetComponent<CharacterControler>().speed = 0;
        player.GetComponent<CharacterAttack>().AttackSpeed = 0;
        player.GetComponent<CharacterAttack>().OnCooldown = true;
    }
}
