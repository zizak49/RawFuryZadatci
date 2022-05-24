using UnityEngine;

public class PickUpState : State
{
    public override void EnterState(AIMovement aIMovement)
    {
        aIMovement.currentDestionation = aIMovement.carryBlock.color == Block.BlockColor.Red ?
            aIMovement.redBoxContainer : aIMovement.blueBoxContainer;
    }

    public override void OnTriggerEnter(AIMovement aIMovement, Collider2D collider2D)
    {
        //throw new System.NotImplementedException();
    }

    public override void UpdateState(AIMovement aIMovement)
    {
        if (aIMovement.ReachedDestination()) 
        {
            aIMovement.SwitchState(aIMovement.dropOffState);        
        }      
    }
}
