using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public enum TileTypePlacement
    {
        Start,
        End,
        Null
    }

    public TileTypePlacement placement = TileTypePlacement.Null;

    [SerializeField] private GameObject tilePrefab;
    public Tile[,] grid;
    [SerializeField] public int xSize;
    [SerializeField] public int ySize;

    private Tile startTile = null;
    private Tile endTile = null;
    private Tile currentTile = null;

    [SerializeField] private Task2UI_Controller uiController;

    private static GridManager _instance;
    public static GridManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        grid = new Tile[xSize,ySize];
        GenerateGrid(xSize, ySize);
    }

    private void GenerateGrid(int xSize, int ySize) 
    {        
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                GameObject newTile = Instantiate(tilePrefab, new Vector2(0,0), Quaternion.identity, transform);
                newTile.transform.position = new Vector2(x,y);
                newTile.gameObject.name = "Tile: " + x + "|" + y;

                Tile tile = newTile.GetComponent<Tile>();
                tile.posX = x;
                tile.posY = y;

                grid[x, y] = tile;
            }
        }      
    }

    public void SetStartEndTile(Tile tile) 
    {
        if (placement == TileTypePlacement.Start)
        {
            if (startTile == null)
            {
                startTile = tile;
                startTile.ColorStart();
                return;
            }
            startTile.ColorPath();
            startTile = tile;
            startTile.ColorStart();
        }

        if (placement == TileTypePlacement.End) 
        {
            if (endTile == null)
            {
                endTile = tile;
                endTile.ColorEnd();
                return;
            }
            endTile.ColorPath();
            endTile = tile;
            endTile.ColorEnd();
        }

        if (startTile != null && endTile != null)
        {
            uiController.EnableFindPathButton();
        }
    }

    public void CalculatePath() 
    {
        float distance = Vector2.Distance(startTile.transform.position, endTile.transform.position);

        Debug.Log(distance);

        currentTile = startTile;

        currentTile.visited = true;

        currentTile.CheckNeighbours();
        



    }

    private void CheckNeighbours(Tile currentTile) 
    {
        Tile top = grid[currentTile.posX, currentTile.posY + 1];
        Tile right = grid[currentTile.posX + 1, currentTile.posY];
        Tile bottom = grid[currentTile.posX, currentTile.posY - 1];
        Tile left = grid[currentTile.posX - 1, currentTile.posY];

        top.ColorVisited();
        right.ColorVisited();
        bottom.ColorVisited();
        left.ColorVisited();
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


