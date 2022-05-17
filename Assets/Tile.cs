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
        renderer.color = onMouseOver;
    }

    private void OnMouseDown()
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
}
