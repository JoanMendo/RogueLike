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
    public GameObject previousLevel;
    public List<GameObject> enemyList = new List<GameObject>();
    public GameObject Cloud;
    private Vector2 offset;

    void Awake()
    {  
        instance = this;
    }

    public void checkList()
    {
        if (enemyList.Count == 0)
        {
            createRandomOffset();
            Instantiate(Cloud, new Vector3(player.transform.position.x, player.transform.position.y + 15, 0), Quaternion.identity);
        }
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
