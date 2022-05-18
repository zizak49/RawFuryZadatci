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

    private int _xSize = 100;
    private int _ySize = 100;

    private Tile[,] _maze;
    private Tile _startTile = null;
    private Tile _endTile = null;

    private bool _useDiagonal = false;

    [SerializeField] private GameObject _tilePrefab;
    [SerializeField] private Task2UIController _uiController;

    public int XSize { get => _xSize; set => _xSize = value; }
    public int YSize { get => _ySize; set => _ySize = value; }
    public bool UseDiagonal { get => _useDiagonal; set => _useDiagonal = value; }

    public void GenerateMaze()
    {
        _maze = new Tile[_xSize, _ySize];

        for (int x = 0; x < _xSize; x++)
        {
            for (int y = 0; y < _ySize; y++)
            {
                GameObject newTile = Instantiate(_tilePrefab, new Vector2(0, 0), Quaternion.identity, transform);
                newTile.transform.position = new Vector2(x, y);
                newTile.gameObject.name = "Tile: " + x + "|" + y;

                Tile tile = newTile.GetComponent<Tile>();
                tile.PosX = x;
                tile.PosY = y;

                _maze[x, y] = tile;
            }
        }
        GetTileNeighbours();
    }

    public void CalculatePath()
    {
        Pathfinding pathfinding = new Pathfinding();
        pathfinding.FindPath(_startTile, _endTile);
    }

    public void SetStartEndTile(Tile tile)
    {
        if (placement == TileTypePlacement.Start)
        {
            if (_startTile == null)
            {
                _startTile = tile;
                _startTile.ColorStart();
                return;
            }
            _startTile.ColorPath();
            _startTile = tile;
            _startTile.ColorStart();
        }

        if (placement == TileTypePlacement.End)
        {
            if (_endTile == null)
            {
                _endTile = tile;
                _endTile.ColorEnd();
                return;
            }
            _endTile.ColorPath();
            _endTile = tile;
            _endTile.ColorEnd();
        }

        if (_startTile != null && _endTile != null)
        {
            _uiController.EnableFindPathButton();
        }
    }

    // Returnes true if tile is in bodred of maze and not a wall
    private bool IsValidTile(int x, int y) 
    {
        if (x >= 0 && x < XSize && y >= 0 && y < YSize) 
        {
            if (_maze[x,y].IsWall)
                return false;

            return true;
        }
        return false;
    }
    
    // probaj maknut ifove
    public void GetTileNeighbours()
    {
        for (int x = 0; x < XSize; x++)
        {
            for (int y = 0; y < YSize; y++)
            {
                Tile tile = _maze[x,y];

                // Top
                if (IsValidTile(x, y + 1))
                    tile.Neighbours.Add(_maze[tile.PosX, tile.PosY + 1]);
                // Bottom
                if (IsValidTile(x, y - 1))
                    tile.Neighbours.Add(_maze[tile.PosX, tile.PosY - 1]);
                // Right
                if (IsValidTile(x + 1, y))
                    tile.Neighbours.Add(_maze[tile.PosX + 1, tile.PosY]);
                // Left
                if (IsValidTile(x - 1, y))
                    tile.Neighbours.Add(_maze[tile.PosX - 1, tile.PosY]);

                if (UseDiagonal)
                {
                    // Top right
                    if (IsValidTile(x + 1, y + 1))
                        tile.Neighbours.Add(_maze[tile.PosX + 1, tile.PosY + 1]);
                    // Top left
                    if (IsValidTile(x - 1, y + 1))
                        tile.Neighbours.Add(_maze[tile.PosX - 1, tile.PosY + 1]);
                    // Bottom right
                    if (IsValidTile(x - 1, y - 1))
                        tile.Neighbours.Add(_maze[tile.PosX - 1, tile.PosY - 1]);
                    // Bottom left
                    if (IsValidTile(x + 1, y - 1))
                        tile.Neighbours.Add(_maze[tile.PosX + 1, tile.PosY - 1]);
                }
            }
        }
    }

    public Tile[,] GetMaze() 
    {
        return _maze;
    }
}


