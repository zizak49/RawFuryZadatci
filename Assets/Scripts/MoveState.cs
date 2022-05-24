using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    public override void EnterState(AIMovement aIMovement)
    {
        aIMovement.currentDestionation = FindClosestBlockToCarry(aIMovement.blocks).gameObject;
    }

    public override void OnTriggerEnter(AIMovement aIMovement, Collider2D collider2D)
    {
        //throw new System.NotImplementedException();
    }

    public override void UpdateState(AIMovement aIMovement)
    {
        if (aIMovement.ReachedDestination())
        {
            aIMovement.PickUpBox(aIMovement.currentDestionation);
            aIMovement.SwitchState(aIMovement.pickUpState);
        }
    }

    private Block FindClosestBlockToCarry(List<Block> blocks)
    {
        float distance = float.MaxValue;
        Block closestBlock = blocks[0];

        foreach (Block item in blocks)
        {
            if (item.distanceToContainer < distance)
            {
                distance = item.distanceToContainer;
                closestBlock = item;
            }
        }
        Debug.Log(closestBlock.name + " " + closestBlock.distanceToContainer);

        return closestBlock;
    }
}
