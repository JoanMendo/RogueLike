using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject levelPrefab;
    public GameObject player;
    public List<Vector2> levelPositions;
    public GameObject currentLevel;
    public List<GameObject> enemyList = new List<GameObject>();
    public GameObject Cloud;
    private GameObject cloud;
    private Vector2 offset;

    void Awake()
    {  
        instance = this;
    }

    public void checkList()
    {
        if (enemyList.Count == 0)
        {
            cloud = Instantiate(Cloud, new Vector3(player.transform.position.x, player.transform.position.y + 15, 0), Quaternion.identity);
            StartCoroutine(CloudMovement());
        }
    }

    public IEnumerator CloudMovement()
    {
        createRandomOffset();
        //Que la nube se deslice hasta la posicion del jugador
        while (Vector3.Distance(cloud.transform.position,player.transform.position + new Vector3(0,-0.2f,0)) > 0.05f)
        {
            cloud.transform.position = Vector3.MoveTowards(cloud.transform.position, player.transform.position + new Vector3(0, -0.2f, 0), 0.15f);
            yield return new WaitForSeconds(0.01f);
        }
        
        Vector2 newPosition = currentLevel.GetComponent<ProceduralTilemap>().playerPosition;
        //Add one to teh cloud and the player sorting order
        player.GetComponent<SortingGroup>().sortingOrder = 4;
        while (Vector3.Distance(cloud.transform.position, newPosition) > 0.2f)
        {
            cloud.transform.position = Vector3.MoveTowards(cloud.transform.position, newPosition, 0.15f);
            player.transform.position = cloud.transform.position + new Vector3 (0,-0.2f,0);
            yield return new WaitForSeconds(0.01f);
        }
        Destroy(cloud);
        player.GetComponent<SortingGroup>().sortingOrder = 3;
    }

    public void createRandomOffset()
    {
        bool isRepeated;
        do
        {
            isRepeated = false;
            float random = Random.Range(1, 5);
            switch (random)
            {
                case 1:
                    offset = new Vector2(50, 0); //Derecha
                    break;
                case 2:
                    offset = new Vector2(-50, 0); // Izquierda
                    break;
                case 3:
                    offset = new Vector2(0, 50); // Arriba
                    break;
                case 4:
                    offset = new Vector2(0, -50); // Abajo
                    break;
            }
            
            foreach (Vector2 position in levelPositions)
            {
                if (Vector2.Distance(position, (Vector2)currentLevel.transform.parent.parent.position + offset) < 0.2f)
                {
                    isRepeated = true;
                    break;
                }
            }


        } while (isRepeated);

        currentLevel.GetComponent<ProceduralTilemap>().makeNewLevel(offset);

    }
}
