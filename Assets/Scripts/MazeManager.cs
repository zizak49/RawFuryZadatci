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
                tile.PosX = x;
                tile.PosY = y;

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
        if (tile.IsWall)
            return false;

        if (tile.PosX >= 0 && tile.PosX < _xSize && tile.PosY >= 0 && tile.PosY + 1 < _ySize)
            return true;

        return false;
    }
    
    // probaj maknut ifove
    public void GetTileNeighbours(Tile tile)
    {
        // Top
        if (IsValidTile(_maze[tile.PosX,tile.PosY + 1]))
            tile.Neighbours.Add(_maze[tile.PosX, tile.PosY + 1]);
        // Bottom
        if (IsValidTile(_maze[tile.PosX, tile.PosY - 1]))
            tile.Neighbours.Add(_maze[tile.PosX, tile.PosY - 1]);
        // Right
        if (IsValidTile(_maze[tile.PosX + 1, tile.PosY]))
            tile.Neighbours.Add(_maze[tile.PosX + 1, tile.PosY]);
        // Left
        if (IsValidTile(_maze[tile.PosX - 1, tile.PosY]))
            tile.Neighbours.Add(_maze[tile.PosX - 1, tile.PosY]);

        if (useDiagonal)
        {
            // Top right
            if (IsValidTile(_maze[tile.PosX + 1, tile.PosY + 1]))
                tile.Neighbours.Add(_maze[tile.PosX + 1, tile.PosY + 1]);
            // Top left
            if (IsValidTile(_maze[tile.PosX - 1, tile.PosY + 1]))
                tile.Neighbours.Add(_maze[tile.PosX - 1, tile.PosY + 1]);
            // Bottom right
            if (IsValidTile(_maze[tile.PosX - 1, tile.PosY - 1]))
                tile.Neighbours.Add(_maze[tile.PosX - 1, tile.PosY - 1]);
            // Bottom left
            if (IsValidTile(_maze[tile.PosX + 1, tile.PosY - 1]))
                tile.Neighbours.Add(_maze[tile.PosX + 1, tile.PosY - 1]);
        }
    }

    public Tile[,] GetMaze() { return _maze; }
}


