using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool visited = false;
    public bool isWall = false;

    public int posX;
    public int posY;

    public List<Tile> neigbors;

    [SerializeField] private Color onMouseOver;
    private Color color;
    private SpriteRenderer renderer;

    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        color = renderer.color;
        neigbors = new List<Tile>();
    }
    public void CheckNeighbours()
    {
        Tile[,] grid = GridManager.Instance.grid;
        int x = GridManager.Instance.xSize;
        int y = GridManager.Instance.ySize;

        // check borders and wall
        if (posY+1 < y)
        {
            Tile top = grid[posX, posY + 1];
            if (!top.isWall)
            {
                top.ColorVisited();
                neigbors.Add(top);
            }
        }
        if (posX+1 < x)
        {
            Tile right = grid[posX + 1, posY];
            if (!right.isWall)
            {
                right.ColorVisited();
                neigbors.Add(right);
            }
        }
        if (posY-1 >= 0)
        {
            Tile bottom = grid[posX, posY - 1];
            if (!bottom.isWall)
            {
                bottom.ColorVisited();
                neigbors.Add(bottom);
            }
        }
        if (posX-1 >=0)
        {
            Tile left = grid[posX - 1, posY];
            if (!left.isWall)
            {
                left.ColorVisited();
                neigbors.Add(left);
            }
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButton(0))
        {
            if (color == Color.white)
            {
                renderer.color = Color.black;
                isWall = true;
            }
            else
            {
                renderer.color = Color.white;
                isWall = false;
            }
        }

        if (Input.GetMouseButton(1))
        {
            GridManager.Instance.SetStartEndTile(this);
        }
    }

    public void ColorStart() 
    {
        renderer.color = Color.green;
    }

    public void ColorEnd() 
    {
        renderer.color = Color.red;
    }

    public void ColorPath() 
    {
        renderer.color = Color.white;
    }

    public void ColorVisited() { renderer.color = Color.yellow; }
}
