using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    private State currentState;
    public SearchState searchState = new SearchState();
    public PickUpState pickUpState = new PickUpState();
    public DropOffState dropOffState = new DropOffState();
    public MoveState moveState = new MoveState();

    [SerializeField] private float speed;

    [SerializeField] public GameObject blueBoxContainer;
    [SerializeField] public GameObject redBoxContainer;

    [SerializeField] public GameObject currentDestionation;
    [SerializeField] private float destinationReachedDistance;

    [SerializeField] private GameObject boxHolder;
    public Block carryBlock;
    private bool carrying = false;

    private float distanceToBlueCon;
    private float distanceToRedCon;

    public List<Block> blocks = new List<Block>();

    private void Start()
    {
        currentState = searchState;
        currentState.EnterState(this);
    }

    private void FixedUpdate()
    { 
        distanceToBlueCon = Vector2.Distance(transform.position, blueBoxContainer.transform.position);
        distanceToRedCon = Vector2.Distance(transform.position, redBoxContainer.transform.position);

        currentState.UpdateState(this);

        transform.position = Vector2.MoveTowards(transform.position, currentDestionation.transform.position, Time.deltaTime * speed);
    }

    public void SwitchState(State newState) 
    {
        currentState = newState;
        Debug.Log(newState);
        newState.EnterState(this);
    }

    public bool ReachedDestination()
    {
        return Vector2.Distance(transform.position, currentDestionation.transform.position) < destinationReachedDistance;
    }

    public void PickUpBox(GameObject block) 
    {
        carrying = true;
        carryBlock = block.GetComponent<Block>();

        carryBlock.transform.parent = boxHolder.transform;        
        carryBlock.transform.position = boxHolder.transform.position;
    }

    public void DropOffBox() 
    {
        carrying = false;
        blocks.Remove(carryBlock);

        carryBlock.SetCollider(false);
        carryBlock.transform.parent = currentDestionation.transform;

        carryBlock.transform.position = new Vector2(currentDestionation.transform.position.x + Random.Range(-0.5f,0.5f),
            currentDestionation.transform.position.y + Random.Range(-0.5f, 0.5f));

        carryBlock = null;

        currentDestionation = null;

    }

    public void Search() 
    {
        currentDestionation = distanceToBlueCon > distanceToRedCon ? blueBoxContainer : redBoxContainer;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Block"))
        {
            Block block = other.GetComponent<Block>();
            if (!blocks.Contains(block))
            {
                block.distanceToContainer = CalculateDistanceToCon(block);
                blocks.Add(block);
            }

            if (!carrying)
            {
                currentState.OnTriggerEnter(this, other);                
            }

        }
    }

    private float CalculateDistanceToCon(Block block) 
    {
        if (block.color == Block.BlockColor.Red)
        {
            return Vector2.Distance(block.transform.position, redBoxContainer.transform.position);
        }
        return Vector2.Distance(block.transform.position, blueBoxContainer.transform.position);
    }
}

