using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public enum TileType
    {
        Start,
        End,
        Walkable,
        Path,
        Wall
    }

    public TileType tileType;

    private Dictionary<TileType, Color> tileTypesByColor = new Dictionary<TileType, Color>();

    private bool _visited = false;

    private int _gCost;
    private int _hCost;
    private int _fCost;

    private int _posX;
    private int _posY;

    private List<Tile> _neighbours = new List<Tile>();
    private Tile _parent;

    public int PosX { get => _posX; set => _posX = value; }
    public int PosY { get => _posY; set => _posY = value; }
    public int GCost { get => _gCost; set => _gCost = value; }
    public int HCost { get => _hCost; set => _hCost = value; }
    public int FCost { get => _gCost + HCost; }
    public Tile Parent { get => _parent; set => _parent = value; }
    public List<Tile> Neighbours { get => _neighbours; set => _neighbours = value; }
    public bool Visited { get => _visited; set => _visited = value; }

    private SpriteRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        InitColorTileTypeValues();
        SetTileColorByType(tileType);
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButton(0))
        {
            if (tileType == TileType.Walkable)
            {
                SetTileColorByType(TileType.Wall);
                UpdateNeighbours();
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            MazeManager mazeManager = GetComponentInParent<MazeManager>();

            if (mazeManager.placement == MazeManager.TileTypePlacement.Null)
                return;

            if (mazeManager.placement == MazeManager.TileTypePlacement.Start)
                mazeManager.SetStartTile(this);
            else               
                mazeManager.SetEndTile(this);
        }
    }

    private void UpdateNeighbours()
    {
        if (tileType == TileType.Wall)
        {
            foreach (Tile item in _neighbours)
            {
                if (item.Neighbours.Contains(this))
                {
                    item.Neighbours.Remove(this);
                }
            }
        }
    }

    public void RemoveWallsFromNeighbours() 
    {
        Neighbours.RemoveAll(x => x.tileType == TileType.Wall);
    }

    public void SetTileColor(Color newColor) 
    {
        _renderer.color = newColor;
    }

    public void SetTileColorByType(TileType newTileType)
    {
        tileType = newTileType;
        _renderer.color = tileTypesByColor[tileType];
    }

    private void InitColorTileTypeValues() 
    {
        tileTypesByColor.Add(TileType.Start, Color.green);
        tileTypesByColor.Add(TileType.End, Color.red);
        tileTypesByColor.Add(TileType.Walkable, Color.white);
        tileTypesByColor.Add(TileType.Wall, Color.black);
        tileTypesByColor.Add(TileType.Path, Color.yellow);
    }
}
