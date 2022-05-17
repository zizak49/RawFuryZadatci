using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private GameObject tilePrefab;
    private GameObject[] grid;
    [SerializeField] private int xSize;
    [SerializeField] private int ySize;

    private void Start()
    {
        GenerateGrid(xSize, xSize);
    }

    private void GenerateGrid(int x, int y) 
    {        
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                GameObject newTile = Instantiate(tilePrefab, new Vector2(0,0), Quaternion.identity, transform);
                newTile.transform.position = new Vector2(i,j);
                newTile.gameObject.name = "Tile: " + i + "|" + j;
            }
        }      
    }

    /*private void GenerateBorder() 
    {
        for (int i = -1; i < xSize; i++)
        {
            GameObject newTile = Instantiate(tilePrefab, new Vector2(0, 0), Quaternion.identity, transform);
            newTile.transform.position = new Vector2(i, -1);
            Tile tile = newTile.GetComponent<Tile>();
            tile.isWall = true;

            newTile.GetComponent<SpriteRenderer>().color = Color.black;

            for (int j = -1; j < ySize; j++)
            {
                newTile = Instantiate(tilePrefab, new Vector2(0, 0), Quaternion.identity, transform);
                newTile.transform.position = new Vector2(-1, j);
                tile = newTile.GetComponent<Tile>();
                tile.isWall = true;

                newTile.GetComponent<SpriteRenderer>().color = Color.black;
            }
        }

    }*/
}
