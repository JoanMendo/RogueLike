using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ProceduralTilemap : MonoBehaviour
{
    [Header("Tilemap Settings")]
    public Tilemap tilemap;

    [Header("Grass Tiles")]
    public TileBase tileCenterGrass;
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

    [Header("Map Dimensions")]
    public int minWidth = 10;
    public int maxWidth = 20;
    public int minHeight = 5;
    public int maxHeight = 15;
    public int transitionHeight = 3; // Punto de transición de acantilado a hierba

    [Header("Generation Settings")]
    public float heightChangeProbability = 0.5f; // Probabilidad de cambio de altura en cada columna


    private int[] bottomHeights;
    private int[] topHeights;
    private int  width;


    private void Start()
    {
        GenerateTilemap();
        PlaceTiles();
    }

    void GenerateTilemap()
    {
        width = Random.Range(minWidth, maxWidth + 1);  //Ancho aleatorio de todo el tilemap

        
        bottomHeights = new int[width]; //Definimos el array de alturas con ell width total
        topHeights = new int[width]; //Definimos el array de alturas con ell width total
        bottomHeights[0] = 0;
        topHeights[0] = Random.Range(minHeight, maxHeight + 1); //Definimos la primera columna, que es necesario antes del bucle que hay a continuación

        
        for (int x = 1; x < width; x++) 
        {
            topHeights[x] = topHeights[x - 1]; //EMpezamos el bucle en 1, ya que la primera columna ya la hemos definido y sino daria error
            bottomHeights[x] = bottomHeights[x - 1];

            // Decidir aleatoriamente si aumentar o disminuir la altura
            if (Random.value < heightChangeProbability)
            {
                int change = Random.Range(-1, 2); // -1, 0, 1
                bottomHeights[x] = Mathf.Clamp(bottomHeights[x] + change, minHeight, maxHeight); //Clamp para que no se salga de los limites
                change = Random.Range(-1, 2); // -1, 0, 1
                topHeights[x] = Mathf.Clamp(topHeights[x] + change, minHeight, maxHeight);
                Debug.Log("Top Height: " + topHeights[x]);
                Debug.Log("Bottom Height: " + bottomHeights[x]);
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

                    if (bottomHeights[x] < bottomHeights[x + 1])
                    {
                        tilemap.SetTile(tilePosition, tileCliffRightBottom);
                    }
                    else if (bottomHeights[x] > bottomHeights[x + 1])
                    {
                        tilemap.SetTile(tilePosition, tileCliffLeftBottom);
                    }
                    else
                    {
                        tilemap.SetTile(tilePosition, tileCliffBottom);
                    }
                }

                else if (y < bottomHeights[x] + transitionHeight)
                {
                    if (bottomHeights[x] < bottomHeights[x + 1])
                    {
                        tilemap.SetTile(tilePosition, tileCliffLeft);
                    }
                    else if (bottomHeights[x] > bottomHeights[x + 1])
                    {
                        tilemap.SetTile(tilePosition, tileCliffRight);
                    }
                    else
                    {
                        tilemap.SetTile(tilePosition, tileCliff);
                    }
                }

                else if (y == bottomHeights[x] + transitionHeight)
                {
                    if (bottomHeights[x] < bottomHeights[x + 1])
                    {
                        tilemap.SetTile(tilePosition, tileBottomRightGrass);
                    }
                    else if (bottomHeights[x] > bottomHeights[x + 1])
                    {
                        tilemap.SetTile(tilePosition, tileBottomLeftGrass);
                    }
                    else
                    {
                        tilemap.SetTile(tilePosition, tileBottomGrass);
                    }
                }

                else if (y > bottomHeights[x] + transitionHeight)
                {
                    if (y == topHeights[x])
                    {
                        if (topHeights[x] < topHeights[x + 1])
                        {
                            tilemap.SetTile(tilePosition, tileTopLeftGrass);
                        }
                        else if (topHeights[x] > topHeights[x + 1])
                        {
                            tilemap.SetTile(tilePosition, tileTopRightGrass);
                        }
                        else
                        {
                            tilemap.SetTile(tilePosition, tileTopGrass);
                        }
                    }

                    else if (x == 0)
                    {
                        tilemap.SetTile(tilePosition, tileLeftGrass);
                    }
                    else if (x == width)
                    {
                        tilemap.SetTile(tilePosition, tileRightGrass);
                    }
                    

                    else
                    {
                        tilemap.SetTile(tilePosition, tileCenterGrass);
                    }
                }
                
            }
        }
    }
}