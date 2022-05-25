using UnityEngine;

public class PickUpState : State
{
    public override void EnterState(AIMovement aIMovement)
    {
        aIMovement.CurrentDestionation = aIMovement.CarryBlock.color == Block.BlockColor.Red ?
            aIMovement.RedBoxContainer : aIMovement.BlueBoxContainer;
    }

    public override void OnTriggerEnter(AIMovement aIMovement, Collider2D collider2D){}

    public override void UpdateState(AIMovement aIMovement)
    {
        if (aIMovement.ReachedDestination()) 
        {
            aIMovement.SwitchState(aIMovement.DropOffState);        
        }      
    }
}
