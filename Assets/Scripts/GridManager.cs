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

    [SerializeField] public int xSize;
    [SerializeField] public int ySize;

    public Tile[,] maze;

    private Tile startTile = null;
    private Tile endTile = null;
    private Tile currentTile = null;

    [SerializeField] private GameObject tilePrefab;

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
        maze = new Tile[xSize, ySize];
        GenerateGrid(xSize, ySize);
    }

    private void GenerateGrid(int xSize, int ySize)
    {
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                GameObject newTile = Instantiate(tilePrefab, new Vector2(0, 0), Quaternion.identity, transform);
                newTile.transform.position = new Vector2(x, y);
                newTile.gameObject.name = "Tile: " + x + "|" + y;

                Tile tile = newTile.GetComponent<Tile>();
                tile.posX = x;
                tile.posY = y;

                maze[x, y] = tile;
            }
        }
    }

    public void CalculatePath()
    {
        Debug.Log("calc");

        Pathfinding pathfinding = new Pathfinding();
        pathfinding.FindPath(startTile, endTile);
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

    public List<Tile> GetTileNeighbours(Tile tile)
    {
        List<Tile> neighbours = new List<Tile>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;

                int checkX = tile.posX + x;
                int checkY = tile.posY + y;

                if (checkX >= 0 && checkX < xSize && checkY >= 0 && checkY < ySize)
                {
                    neighbours.Add(maze[checkX, checkY]);
                }
            }
        }        
        return neighbours;
    }
}


