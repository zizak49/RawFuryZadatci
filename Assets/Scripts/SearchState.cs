using UnityEngine;

public class SearchState : State
{
    public override void EnterState(AIMovement aIMovement)
    {
        aIMovement.Search(); 
    }

    public override void OnTriggerEnter(AIMovement aIMovement, Collider2D collider2D)
    {
        aIMovement.CarryBlock = collider2D.GetComponent<Block>();
        aIMovement.SwitchState(aIMovement.PickUpState);
        aIMovement.PickUpBox(collider2D.gameObject);
    }

    public override void UpdateState(AIMovement aIMovement)
    {
        if (aIMovement.ReachedDestination())
            aIMovement.Search();
    }
}
