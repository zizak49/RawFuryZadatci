using UnityEngine;

public class Tile : MonoBehaviour
{
    private SpriteRenderer renderer;
    public bool isWall = false;

    [SerializeField] private Color onMouseOver;
    private Color color;

    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        color = renderer.color;
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
}
