using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;


public class ProceduralTilemap : MonoBehaviour
{
    [Header("Tilemap Settings")]
    public Tilemap tilemap;
    public Tilemap collider;
    public GameObject levelPrefab;

    [Header("Grass Tiles")]
    public TileBase[] centerGrassDecoration;
    public TileBase centerGrass;
    public TileBase tileTopGrass;
    public TileBase tileBottomGrass;
    public TileBase tileLeftGrass;
    public TileBase tileRightGrass;
    public TileBase tileTopLeftGrass;
    public TileBase tileTopRightGrass;
    public TileBase tileBottomLeftGrass;
    public TileBase tileBottomRightGrass;

    [Header("Cliff Tiles")]
    public TileBase tileCliff;
    public TileBase tileCliffBottom;
    public TileBase tileCliffLeft;
    public TileBase tileCliffRight;
    public TileBase tileCliffRightBottom;
    public TileBase tileCliffLeftBottom;

    [Header("Collider Tiles")]

    public TileBase leftUp;
    public TileBase rightUp;
    public TileBase leftDown;
    public TileBase rightDown;


    [Header("Map Dimensions")]
    public int minWidth = 10;
    public int maxWidth = 20;
    public int minHeight = 5;
    public int maxHeight = 15;
    public int transitionHeight = 3; // Punto de transición de acantilado a hierba

    [Header("Generation Settings")]
    public float heightChangeProbability = 0.5f; // Probabilidad de cambio de altura en cada columna
    [Header("Decorations")]
    public GameObject[] bigDecorations;
    public GameObject[] smallDecorations;

    [Header("Prefabs")]
    public GameObject slimePrefab;
    public GameObject CharacterPrefab;

    private int[] bottomHeights;
    private int[] topHeights;
    private List<Vector2> tilesPlanas = new List<Vector2>();
    private List<Vector2> tilesNotUsed = new List<Vector2>();
    private int  width;

    public Vector2 playerPosition;




    private void Start()
    { 
        GenerateTilemap();
        PlaceTiles();
        generateDecorations();
        generateEntities();
        GameManager.instance.currentLevel = gameObject;
        levelPrefab  = GameManager.instance.levelPrefab;
    }

    void GenerateTilemap()
    {

        width = Random.Range(minWidth, maxWidth + 1);  //Ancho aleatorio de todo el tilemap
        bottomHeights = new int[width]; //Definimos el array de alturas con ell width total
        topHeights = new int[width]; //Definimos el array de alturas con ell width total
        bottomHeights[0] = 0;
        topHeights[0] = Random.Range(minHeight, maxHeight + 1); //Definimos la primera columna, que es necesario antes del bucle que hay a continuación


        GameManager.instance.levelPositions.Add(transform.position);

        for (int x = 1; x < width; x++) 
        {
            topHeights[x] = topHeights[x - 1]; //EMpezamos el bucle en 1, ya que la primera columna ya la hemos definido y sino daria error
            bottomHeights[x] = bottomHeights[x - 1];

            // Decidir aleatoriamente si aumentar o disminuir la altura
            if (Random.value < heightChangeProbability && x % 3 == 0 && x < width-6)
            { 
                int change = Random.Range(-1, 2); // -1, 0, 1
                bottomHeights[x] = Mathf.Clamp(bottomHeights[x] + change, -10, minHeight); //Clamp para que no se salga de los limites
                change = Random.Range(-1, 2); // -1, 0, 1
                topHeights[x] = Mathf.Clamp(topHeights[x] + change, minHeight, maxHeight);

            }
        }

      
    }

    void PlaceTiles()
    {

        for (int x = 0; x < bottomHeights.Length -1; x++)
        {
            for (int y = bottomHeights[x]; y < topHeights[x]; y++)
            {
                Vector3Int tilePosition = new Vector3Int(x, y, 0);
                


                if (y == bottomHeights[x])
                {

                   generateCliffTiles(tileCliffLeftBottom, tileCliff, tileCliffRightBottom, x, tilePosition, true);
                    
                }

                else if (y < bottomHeights[x] + transitionHeight)
                {
                    generateCliffTiles(tileCliffLeft, tileCliff, tileCliffRight, x, tilePosition, false);
                }

                else if (y == bottomHeights[x] + transitionHeight)
                {
                    generateCliffTiles(tileBottomLeftGrass, tileBottomGrass, tileBottomRightGrass, x, tilePosition, false);
                }

                else if (y > bottomHeights[x] + transitionHeight)
                {
                    if (y == topHeights[x]-1)
                    {
                        if (x == 0)
                        {
                          
                            tilemap.SetTile(tilePosition, tileTopLeftGrass);
                            collider.SetTile(tilePosition, leftUp);
                        }
                        else if (topHeights[x] > topHeights[x - 1])
                        {
                            tilemap.SetTile(tilePosition, tileTopLeftGrass);
                            collider.SetTile(tilePosition, leftUp);

                        }
                        else if (x == topHeights.Length - 2)
                        {
                            tilemap.SetTile(tilePosition, tileTopRightGrass);
                            collider.SetTile(tilePosition, rightUp);
                        }
                        else if (topHeights[x] > topHeights[x + 1])
                        {
                            tilemap.SetTile(tilePosition, tileTopRightGrass);
                            collider.SetTile(tilePosition, rightUp);
                        }
                        else
                        {
                            tilemap.SetTile(tilePosition, tileTopGrass);
                            tilePosition.y++;
                            collider.SetTile(tilePosition, tileCliff);
                        }
                    }

                    else if (x == 0)
                    {
                        tilemap.SetTile(tilePosition, tileLeftGrass);
                        tilePosition.x--;
                        collider.SetTile(tilePosition, tileCliff);
                    }
                    else if (x == width-2)
                    {
                        tilemap.SetTile(tilePosition, tileRightGrass);
                        tilePosition.x++;
                        collider.SetTile(tilePosition, tileCliff);
                    }
                    

                    else
                    {
                        if (x > 2 && x < width-2 && y > transitionHeight + 1 && y < topHeights[x] - 1) 
                        tilesPlanas.Add(new Vector2(x, y));
                        int RandomTile = Random.Range(0, 60);
                        if (RandomTile < centerGrassDecoration.Length)
                        {
                            tilemap.SetTile(tilePosition, centerGrassDecoration[RandomTile]);
                        }
                        else
                        {
                            tilemap.SetTile(tilePosition, centerGrass);
                        }
                    }
                }
                
            }
        }
    }

    void generateCliffTiles(TileBase left, TileBase middle, TileBase right, int x, Vector3Int tilePosition, bool placeWater)
    {
        if (x == 0)
        {
            tilemap.SetTile(tilePosition, left);   
            collider.SetTile(tilePosition, leftDown);
        }
        else if (bottomHeights[x] < bottomHeights[x - 1])
        {
            tilemap.SetTile(tilePosition, left);
            collider.SetTile(tilePosition, leftDown);
        }
        else if (x == bottomHeights.Length - 2)
        {
            tilemap.SetTile(tilePosition, right);
            collider.SetTile(tilePosition, rightDown);
        }
        else if (bottomHeights[x] < bottomHeights[x + 1])
        {
            tilemap.SetTile(tilePosition, right);
            collider.SetTile(tilePosition, rightDown);
        }
        else
        {
            tilemap.SetTile(tilePosition,middle);
            tilePosition.y--;
            collider.SetTile(tilePosition, tileCliff);
            if (placeWater)
            {  
                tilemap.SetTile(tilePosition, tileCliffBottom);
            } 
        }
    }

    void generateDecorations()
    {

        foreach (Vector2 tile in tilesPlanas)
        {
            float random = Random.Range(0, 120);
            if (random < 2)
            {
                int randomIndex = Random.Range(0, bigDecorations.Length);
                GameObject decoration = Instantiate(bigDecorations[randomIndex], tile , Quaternion.identity);
                decoration.transform.parent = transform;
                decoration.transform.position += (Vector3)tilemap.transform.position;


            }
            else if (random < 6)
            {
                int randomIndex = Random.Range(0, smallDecorations.Length);
                GameObject decoration = Instantiate(smallDecorations[randomIndex], tile , Quaternion.identity);
                decoration.transform.parent = transform;
                decoration.transform.position += (Vector3)tilemap.transform.position;

            }
            else
            {
               tilesNotUsed.Add(tile); 
            }
        }
    }

    void generateEntities()
    {
        playerPosition = tilesNotUsed[Random.Range(0, tilesNotUsed.Count)];
        playerPosition += (Vector2)tilemap.transform.position;
        if (GameManager.instance.player == null)
        {
            GameObject player = Instantiate(CharacterPrefab, playerPosition, Quaternion.identity);
            GameObject.Find("Main Camera").GetComponent<CameraFollow>().player = player.transform;
        }
       
        tilesNotUsed.Remove(playerPosition);
        for (int i = 0; i < Random.Range(3f, 5f); i++)
        {
            Vector2 slimePosition = tilesNotUsed[Random.Range(0, tilesNotUsed.Count)];
            slimePosition += (Vector2)tilemap.transform.position;
            GameObject enemy = Instantiate(slimePrefab, slimePosition, Quaternion.identity);
            GameManager.instance.enemyList.Add(enemy);
            tilesNotUsed.Remove(slimePosition);
        }

    }

    public void makeNewLevel(Vector2 offset)
    {

        Instantiate(levelPrefab, new Vector3(transform.parent.parent.position.x + offset.x, transform.parent.parent.position.y + offset.y, 0), Quaternion.identity);
       
    }




}