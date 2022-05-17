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
    private GameObject[] grid;
    [SerializeField] private int xSize;
    [SerializeField] private int ySize;

    private Tile startTile = null;
    private Tile endTile = null;

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
        GenerateGrid(xSize, ySize);
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
