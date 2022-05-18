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

    private bool useDiagonal = false;

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

                GetTileNeighbours(tile);
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

    // Returnes true if tile is in bodred of maze and not a wall
    private bool IsValidTile(Tile tile) 
    {
        if (tile.isWall)
            return false;

        if (tile.posX >= 0 && tile.posX < _xSize && tile.posY >= 0 && tile.posY + 1 < _ySize)
            return true;

        return false;
    }
    
    public void GetTileNeighbours(Tile tile)
    {
        // Top
        if (IsValidTile(_maze[tile.posX,tile.posY + 1]))
            tile.neigbors.Add(_maze[tile.posX, tile.posY + 1]);
        // Bottom
        if (IsValidTile(_maze[tile.posX, tile.posY - 1]))
            tile.neigbors.Add(_maze[tile.posX, tile.posY - 1]);
        // Right
        if (IsValidTile(_maze[tile.posX + 1, tile.posY]))
            tile.neigbors.Add(_maze[tile.posX + 1, tile.posY]);
        // Left
        if (IsValidTile(_maze[tile.posX - 1, tile.posY]))
            tile.neigbors.Add(_maze[tile.posX - 1, tile.posY]);

        if (useDiagonal)
        {
            // Top right
            if (IsValidTile(_maze[tile.posX + 1, tile.posY + 1]))
                tile.neigbors.Add(_maze[tile.posX + 1, tile.posY + 1]);
            // Top left
            if (IsValidTile(_maze[tile.posX - 1, tile.posY + 1]))
                tile.neigbors.Add(_maze[tile.posX - 1, tile.posY + 1]);
            // Bottom right
            if (IsValidTile(_maze[tile.posX - 1, tile.posY - 1]))
                tile.neigbors.Add(_maze[tile.posX - 1, tile.posY - 1]);
            // Bottom left
            if (IsValidTile(_maze[tile.posX + 1, tile.posY - 1]))
                tile.neigbors.Add(_maze[tile.posX + 1, tile.posY - 1]);
        }
    }

    public Tile[,] GetMaze() { return _maze; }
}


