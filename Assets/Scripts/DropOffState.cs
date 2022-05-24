using UnityEngine;
public class DropOffState : State
{
    public override void EnterState(AIMovement aIMovement)
    {
        aIMovement.DropOffBox();
        if (aIMovement.blocks.Count != 0)
        {
            aIMovement.SwitchState(aIMovement.moveState);
        }
        else
        {
            aIMovement.SwitchState(aIMovement.searchState);
        }
    }

    public override void OnTriggerEnter(AIMovement aIMovement, Collider2D collider2D)
    {
        //throw new System.NotImplementedException();
    }

    public override void UpdateState(AIMovement aIMovement)
    {
        
    }   
}
