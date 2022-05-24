using UnityEngine;
public class DropOffState : State
{
    public override void EnterState(AIMovement aIMovement)
    {
        aIMovement.DropOffBox();
        if (aIMovement.GetBlocks().Count != 0)
        {
            aIMovement.SwitchState(aIMovement.MoveState);
        }
        else
        {
            aIMovement.SwitchState(aIMovement.SearchState);
        }
    }

    public override void OnTriggerEnter(AIMovement aIMovement, Collider2D collider2D)
    {
        
    }

    public override void UpdateState(AIMovement aIMovement)
    {
        
    }   
}
