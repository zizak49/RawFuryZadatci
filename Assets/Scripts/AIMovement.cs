using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    private State _currentState;
    private SearchState _searchState = new SearchState();
    private PickUpState _pickUpState = new PickUpState();
    private DropOffState _dropOffState = new DropOffState();
    private MoveState _moveState = new MoveState();

    [SerializeField] private float _speed;
    [SerializeField] private GameObject _blockHolder;
    
    private bool _carrying = false;
    private Block _carryBlock;

    [SerializeField] private GameObject _blueBlockContainer;
    [SerializeField] private GameObject _redblockContainer;
    private GameObject _currentDestionation;

    [SerializeField] private float _destinationReachedDistance;
    private float _distanceToBlueContainer;
    private float _distanceToRedContainer;

    private List<Block> _blocks = new List<Block>();

    public GameObject BlueBoxContainer { get => _blueBlockContainer; set => _blueBlockContainer = value; }
    public GameObject RedBoxContainer { get => _redblockContainer; set => _redblockContainer = value; }
    public GameObject CurrentDestionation { get => _currentDestionation; set => _currentDestionation = value; }
    public SearchState SearchState { get => _searchState; set => _searchState = value; }
    public PickUpState PickUpState { get => _pickUpState; set => _pickUpState = value; }
    public DropOffState DropOffState { get => _dropOffState; set => _dropOffState = value; }
    public MoveState MoveState { get => _moveState; set => _moveState = value; }
    public Block CarryBlock { get => _carryBlock; set => _carryBlock = value; }

    private void Start()
    {
        _currentState = _searchState;
        _currentState.EnterState(this);
    }

    private void FixedUpdate()
    { 
        _distanceToBlueContainer = Vector2.Distance(transform.position, _blueBlockContainer.transform.position);
        _distanceToRedContainer = Vector2.Distance(transform.position, _redblockContainer.transform.position);

        _currentState.UpdateState(this);

        transform.position = Vector2.MoveTowards(transform.position, CurrentDestionation.transform.position, Time.deltaTime * _speed);
    }

    public void SwitchState(State newState) 
    {
        _currentState = newState;
        newState.EnterState(this);
    }

    public bool ReachedDestination()
    {
        return Vector2.Distance(transform.position, _currentDestionation.transform.position) < _destinationReachedDistance;
    }

    public void PickUpBox(GameObject block) 
    {
        _carrying = true;
        _carryBlock = block.GetComponent<Block>();

        _carryBlock.transform.parent = _blockHolder.transform;
        _carryBlock.transform.position = _blockHolder.transform.position;
    }

    public void DropOffBox() 
    {
        _carrying = false;
        _blocks.Remove(_carryBlock);

        _carryBlock.SetCollider(false);
        _carryBlock.transform.parent = _currentDestionation.transform;

        _carryBlock.transform.position = new Vector2(_currentDestionation.transform.position.x + Random.Range(-0.5f,0.5f),
            _currentDestionation.transform.position.y + Random.Range(-0.5f, 0.5f));

        _carryBlock = null;
        _currentDestionation = null;
    }

    public void Search() 
    {
        _currentDestionation = _distanceToBlueContainer > _distanceToRedContainer ? BlueBoxContainer : RedBoxContainer;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Block"))
        {
            Block block = other.GetComponent<Block>();
            if (!_blocks.Contains(block))
            {
                block.distanceToContainer = CalculateDistanceToContainer(block);
                _blocks.Add(block);
            }

            if (!_carrying)
            {
                _currentState.OnTriggerEnter(this, other);                
            }
        }
    }

    private float CalculateDistanceToContainer(Block block) 
    {
        if (block.color == Block.BlockColor.Red)
            return Vector2.Distance(block.transform.position, _redblockContainer.transform.position);
        return Vector2.Distance(block.transform.position, _blueBlockContainer.transform.position);
    }

    public List<Block> GetBlocks() 
    {
        return _blocks; 
    }
}

