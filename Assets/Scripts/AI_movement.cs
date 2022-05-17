using UnityEngine;

public class AI_movement : MonoBehaviour
{
    private enum AI_State
    {
        Searching,
        MovingToDestination,
        Carry
    }
    private AI_State state = AI_State.Searching;

    [SerializeField] private float speed;

    [SerializeField] private GameObject blueBoxContainer;
    [SerializeField] private GameObject redBoxContainer;

    [Space(10)]

    [SerializeField] private GameObject currentDestionation;
    private GameObject lastDestionation;

    [SerializeField] private float dropOffDistance;
    [SerializeField] private GameObject boxHolder;
    private Block carryBlock;
    private bool carrying = false;

    private Rigidbody2D rigidbody;

    private bool leftClear = false;
    private bool rightClear = false;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        SetDestination(redBoxContainer);
    }

    private void FixedUpdate()
    {
        // move to target
        transform.position = Vector2.MoveTowards(transform.position, currentDestionation.transform.position, Time.deltaTime * speed);

        // destination reached
        if (Vector2.Distance(transform.position, currentDestionation.transform.position) <= dropOffDistance)
        {
            if (state == AI_State.Carry)
            {
                DropOffBox();
                return;
            }

            // check from current position to red container
            if (state == AI_State.Searching && currentDestionation == redBoxContainer)
            {
                leftClear = true;

                if (leftClear && rightClear)
                {
                    Debug.Log("Done");
                    Time.timeScale = 0;
                }

                SetDestination(blueBoxContainer);
                return;
            }

            // check from current position to blue container
            if (state == AI_State.Searching && currentDestionation == blueBoxContainer)
            {
                rightClear = true;

                if (leftClear && rightClear)
                {
                    Debug.Log("Done");
                    Time.timeScale = 0;
                }

                SetDestination(redBoxContainer);
                return;
            }
        }   
    }

    private void PickUpBox(GameObject block) 
    {
        Debug.Log("PickUp");
        carrying = true;

        ChangeState(AI_State.Carry);

        carryBlock = block.GetComponent<Block>();

        carryBlock.transform.parent = boxHolder.transform;        
        carryBlock.transform.position = boxHolder.transform.position;

        if (carryBlock.color == Block.BlockColor.Blue)
        {
            SetDestination(blueBoxContainer);
        }
        else
        {
            SetDestination(redBoxContainer);
        }
    }

    private void DropOffBox() 
    {
        Debug.Log("DropOff");
        carrying = false;

        carryBlock.SetCollider(false);
        carryBlock.transform.parent = currentDestionation.transform;

        carryBlock.transform.position = new Vector2(currentDestionation.transform.position.x + Random.Range(-0.5f,0.5f),
            currentDestionation.transform.position.y + Random.Range(-0.5f, 0.5f));

        if (currentDestionation == redBoxContainer)
            SetDestination(blueBoxContainer);
        else
            SetDestination(redBoxContainer);

        ChangeState(AI_State.Searching);
    
    }

    private void ChangeState(AI_State newState) 
    {
        Debug.Log(newState);
        state = newState; 
    }

    private void SetDestination(GameObject newDestination)  
    {
        lastDestionation = currentDestionation;
        currentDestionation = newDestination;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Block") && !carrying)
        {
            Debug.Log(collision.gameObject.name);
            PickUpBox(collision.gameObject);
        }
    }
}
