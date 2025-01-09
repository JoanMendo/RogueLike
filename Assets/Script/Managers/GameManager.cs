using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject levelPrefab;
    public GameObject player;
    public List<Vector2> levelPositions;
    public GameObject currentLevel;
    public GameObject previousLevel;
    public List<GameObject> enemyList = new List<GameObject>();
    public List<GameObject> cloudList = new List<GameObject>();
    public GameObject Cloud;
    private Vector2 offset;
    private Canvas coinCanvas;
    public int coins = 0;

    void Awake()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        CharacterControler.onPlayerLoad += addPlayer;
    }

    private void OnDisable()
    {
        CharacterControler.onPlayerLoad -= addPlayer;
    }
    public void checkList()
    {
        if (coinCanvas == null)
        {
            coinCanvas = GameObject.FindGameObjectWithTag("CoinCanvas").GetComponent<Canvas>();
        }
        if ( coinCanvas != null)
        {
            coinCanvas.GetComponentInChildren<TextMeshProUGUI>().text = ($"00{coins.ToString()}");
        }
        if (enemyList.Count == 0)
        {
            createRandomOffset();
            enableAllClouds();
        }
    }

    public void createRandomOffset()
    {
        bool isRepeated;
        do
        {
            isRepeated = false;
            float random = UnityEngine.Random.Range(1, 5);
            switch (random)
            {
                case 1:
                    offset = new Vector2(55, 0); //Derecha
                    break;
                case 2:
                    offset = new Vector2(-55, 0); // Izquierda
                    break;
                case 3:
                    offset = new Vector2(0, 55); // Arriba
                    break;
                case 4:
                    offset = new Vector2(0, -55); // Abajo
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
        GameObject cloud = Instantiate(Cloud, new Vector3(player.transform.position.x, player.transform.position.y + 15, 0), Quaternion.identity);
        cloudList.Add(cloud);
    }

    public void enableAllClouds()
    {
        foreach (GameObject cloud in cloudList)
        {
            cloud.SetActive(true);
            cloud.GetComponent<Cloud>().enableCloud();
        }
    }

    public void disableAllClouds(GameObject currentCloud)
    {
        foreach (GameObject cloud in cloudList)
        {
            if (cloud != currentCloud)
            {
                cloud.GetComponent<Cloud>().disableCloud();
            }
        }
    }

    private void addPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void resetGameManager()
    {
        levelPositions = new List<Vector2>();
        enemyList = new List<GameObject>();
        cloudList = new List<GameObject>();
        coinCanvas = null;
        coins = 0;
    }
}

