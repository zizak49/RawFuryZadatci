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

    [SerializeField, Range(100, 500)] private int _xSize;
    [SerializeField, Range(100, 500)] private int _ySize;

    [SerializeField, Range(10000, 50000)] private int _depth;

    private Tile[,] _maze;
    private Tile _startTile = null;
    private Tile _endTile = null;

    private bool _useDiagonal = false;

    [SerializeField] private GameObject _tilePrefab;
    [SerializeField] private Task2UIController _uiController;

    private Pathfinding pathfinding = new Pathfinding();

    public int XSize { get => _xSize; set => _xSize = value; }
    public int YSize { get => _ySize; set => _ySize = value; }
    public bool UseDiagonal { get => _useDiagonal; set => _useDiagonal = value; }
    public int Depth { get => _depth; set => _depth = value; }

    public void InstantiateMaze(bool createAsWalls)
    {
        _maze = new Tile[_xSize, _ySize];

        for (int x = 0; x < _xSize; x++)
        {
            for (int y = 0; y < _ySize; y++)
            {
                GameObject newTile = Instantiate(_tilePrefab, new Vector2(x, y), Quaternion.identity, transform);
                newTile.gameObject.name = x + "|" + y;

                Tile tile = newTile.GetComponent<Tile>();
                tile.PosX = x;
                tile.PosY = y;

                tile.tileType = createAsWalls ? Tile.TileType.Wall : Tile.TileType.Walkable;

                _maze[x, y] = tile;
            }
        }
        GetTileNeighbours(false);
    }

    public void CalculateMazePath()
    {
        pathfinding.ClearPath();
        pathfinding.FindPath(_startTile, _endTile);
    }

    public void SetStartTile(Tile tile)
    {
        if (placement == TileTypePlacement.Start)
        {
            if (_startTile != null)
            {
                _startTile.SetTileColorByType(Tile.TileType.Walkable);
                _startTile = tile;
                _startTile.SetTileColorByType(Tile.TileType.Start);
                return;
            }
            _startTile = tile;
            _startTile.SetTileColorByType(Tile.TileType.Start);
        }

        if (_startTile != null && _endTile != null)
        {
            _uiController.EnableFindPathButton();
        }
    }

    public void SetEndTile(Tile tile)
    {
        if (placement == TileTypePlacement.End)
        {
            if (_endTile != null)
            {
                _endTile.SetTileColorByType(Tile.TileType.Walkable);
                _endTile = tile;
                _endTile.SetTileColorByType(Tile.TileType.End);
                return;
            }
            _endTile = tile;
            _endTile.SetTileColorByType(Tile.TileType.End);
        }

        if (_startTile != null && _endTile != null)
        {
            _uiController.EnableFindPathButton();
        }
    }
    /// <summary>
    /// Returnes true if tile is in bodrders of maze
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    private bool IsValidTile(int x, int y) 
    {
        if (x >= 0 && x < XSize && y >= 0 && y < YSize)
        {
            return true;
        }
        return false;
    }
    
    public void GetTileNeighbours(bool removeWalls)
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

                if (removeWalls)
                {
                    tile.RemoveWallsFromNeighbours();
                }
            }
        }
    }

    public void GenerateMaze() 
    {
        InstantiateMaze(true);

        Tile start = _maze[Random.Range(0,XSize), Random.Range(0, YSize)];
        start.tileType = Tile.TileType.Walkable;
        start.Visited = true;

        int count = 0;
        while (count != Depth) 
        {
            Tile randTile = start.Neighbours[Random.Range(0,start.Neighbours.Count)];

            if (!randTile.Visited) 
            {
                randTile.Visited = true;
                randTile.tileType = Tile.TileType.Walkable;
            }
            start = randTile;
            count++;
        }
        GetTileNeighbours(true);
    }

    public Tile[,] GetMaze() 
    {
        return _maze;
    }
}


