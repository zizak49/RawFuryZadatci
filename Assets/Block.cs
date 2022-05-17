using System.Collections;
using System.Collections.Generic;
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
    private Rigidbody2D rigidbody;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        if (color == BlockColor.Red)
        {
            spriteRenderer.color = Color.red;
        }
        else
        {
            spriteRenderer.color = Color.blue;

        }
    }

    public void SetRigidyBodyKinematic(bool isTrue) 
    {
        rigidbody.isKinematic = isTrue;
    }
}
