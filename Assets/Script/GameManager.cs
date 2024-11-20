using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject player;
    public List<Vector2> levelPositions;

    public List<GameObject> enemyList = new List<GameObject>();
    public GameObject Cloud;
    private GameObject cloud;
    void Awake()
    {  
        instance = this;
    }

    public void checkList()
    {
        Debug.Log(enemyList.Count);
        if (enemyList.Count == 0)
        {
            cloud = Instantiate(Cloud, new Vector3(player.transform.position.x, player.transform.position.y + 15, 0), Quaternion.identity);
            StartCoroutine(CloudToPlayer());
        }
    }

    public IEnumerator CloudToPlayer()
    {
        //Que la nube se deslice hasta la posicion del jugador
        while (Vector3.Distance(cloud.transform.position, player.transform.position) > 0.1f)
        {
            cloud.transform.position = Vector3.MoveTowards(cloud.transform.position, player.transform.position, 0.1f);
            yield return new WaitForSeconds(0.01f);
        }

    }
}
