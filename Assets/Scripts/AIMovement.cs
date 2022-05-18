using UnityEngine;

public class AIMovement : MonoBehaviour
{
    private enum AIState
    {
        Searching,
        MovingToDestination,
        Carry
    }
    private AIState state = AIState.Searching;

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
        //gleadj polovidu i udaljenosti

        // move to target
        transform.position = Vector2.MoveTowards(transform.position, currentDestionation.transform.position, Time.deltaTime * speed);

        // destination reached
        if (Vector2.Distance(transform.position, currentDestionation.transform.position) <= dropOffDistance)
        {
            if (state == AIState.Carry)
            {
                DropOffBox();
                return;
            }

            // check from current position to red container
            if (state == AIState.Searching && currentDestionation == redBoxContainer)
            {
                leftClear = true;

                CheckIfDone();

                SetDestination(blueBoxContainer);
                return;
            }

            // check from current position to blue container
            if (state == AIState.Searching && currentDestionation == blueBoxContainer)
            {
                rightClear = true;

                CheckIfDone();

                SetDestination(redBoxContainer);
                return;
            }
        }   
    }

    private void PickUpBox(GameObject block) 
    {
        Debug.Log("PickUp");
        carrying = true;

        ChangeState(AIState.Carry);

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

        ChangeState(AIState.Searching);
    
    }

    private void ChangeState(AIState newState) 
    {
        Debug.Log(newState);
        state = newState; 
    }

    private void SetDestination(GameObject newDestination)  
    {
        lastDestionation = currentDestionation;
        currentDestionation = newDestination;
    }

    private void CheckIfDone() 
    {
        if (leftClear && rightClear)
        {
            Debug.Log("Done");
            Task1UIController.Instance.ShowExitButton();
            Time.timeScale = 0;
        }
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
