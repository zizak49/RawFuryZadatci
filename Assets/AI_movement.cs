using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_movement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject destionation;

    [SerializeField] private GameObject boxHolder;

    private Rigidbody2D rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {   
    }

    private void FixedUpdate()
    {
        if (destionation == null) 
        {
            return;
        }

        // move to target
        transform.position = Vector2.MoveTowards(transform.position, destionation.transform.position, Time.deltaTime * speed);

        // destination reached
        if (transform.position.x - destionation.transform.position.x <= 0.3f)
        {
            PickUpBox();
        }
       
    }

    private void PickUpBox() 
    {
        Block block = destionation.GetComponent<Block>();

        block.SetRigidyBodyKinematic(true);

        destionation.transform.parent = boxHolder.transform;        
        destionation = null;
    }
}
