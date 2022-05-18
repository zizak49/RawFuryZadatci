using System.Collections.Generic;
using UnityEngine;

public class MazeManager : MonoBehaviour
{
    public enum TileTypePlacement
    {
        Start,
        End,
        Null
    }

    public TileTypePlacement placement = TileTypePlacement.Null;

    private int _xSize, _ySize;

    private Tile[,] _maze;
    private Tile startTile = null;
    private Tile endTile = null;

    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private Task2UI_Controller uiController;

    private void Start()
    {
        _maze = new Tile[_xSize, _ySize];
        GenerateGrid(_xSize, _ySize);
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

                _maze[x, y] = tile;
                
                //
            }
        }
    }

    public void CalculatePath()
    {
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

                if (checkX >= 0 && checkX < _xSize && checkY >= 0 && checkY < _ySize)
                {
                    neighbours.Add(_maze[checkX, checkY]);
                }
            }
        }        
        return neighbours;
    }

    public Tile[,] GetMaze() { return maze; }
}


