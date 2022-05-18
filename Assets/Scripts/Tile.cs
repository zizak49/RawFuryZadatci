using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool isWall = false;

    public int gCost; 
    public int hCost; 
    public int fCost 
    {
        get 
        { 
            return gCost + hCost; 
        }
    }

    public List<Tile> neighbours = new List<Tile>();

    public Tile parent;

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
            MazeManager.Instance.SetStartEndTile(this);
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

    public void ColorVisited() 
    {
        renderer.color = Color.yellow; 
    }
}
