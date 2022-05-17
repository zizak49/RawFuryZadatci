using UnityEngine;

public class AI_movement : MonoBehaviour
{

    private enum AI_State
    {
        Searching,
        MovingToDestination,
        Carry
    }

    private AI_State state;

    [SerializeField] private float speed;

    [SerializeField] private GameObject blueBoxContainer;
    [SerializeField] private GameObject redBoxContainer;

    [SerializeField] private GameObject currentDestionation;

    [SerializeField] private GameObject boxHolder;

    private Block carryBlock;

    private Rigidbody2D rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        if (currentDestionation == null)
        {
            state = AI_State.Searching;
        }
        else
        {
            state = AI_State.MovingToDestination;
        }
    }

    private void FixedUpdate()
    {
        if (currentDestionation == null) 
        {
            return;
        }

        // move to target
        transform.position = Vector2.MoveTowards(transform.position, currentDestionation.transform.position, Time.deltaTime * speed);

        // destination reached
        if (transform.position.x - currentDestionation.transform.position.x <= 0.3f)
        {
            switch (state)
            {
                case AI_State.MovingToDestination:
                    PickUpBox();
                    break;
                case AI_State.Carry:
                    DropOffBox();
                    break;
                default:
                    break;
            }
        }
       
    }

    private void PickUpBox() 
    {
        Debug.Log("PickUp");

        state = AI_State.Carry;

        carryBlock = currentDestionation.GetComponent<Block>();
        carryBlock.SetRigidyBodyKinematic(true);

        currentDestionation.transform.parent = boxHolder.transform;        
        currentDestionation = null;

        currentDestionation = carryBlock.color == Block.BlockColor.Blue ? blueBoxContainer : redBoxContainer;
    }

    private void DropOffBox() 
    {
        Debug.Log("DropOff");
        state = AI_State.Searching;

        carryBlock.transform.parent = null;
    
    }
}
