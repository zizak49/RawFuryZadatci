using UnityEngine;

public class Block : MonoBehaviour
{
    public enum BlockColor 
    {
        Red,
        Blue
    }

    public BlockColor color;

    private SpriteRenderer spriteRenderer;
    private new BoxCollider2D collider;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        spriteRenderer.color = color == BlockColor.Blue ? Color.blue : Color.red;
    }

    public void SetCollider(bool isTrue) 
    {
        collider.enabled = isTrue;
    }
}
