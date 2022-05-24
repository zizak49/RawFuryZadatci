using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    public override void EnterState(AIMovement aIMovement)
    {
        aIMovement.CurrentDestionation = FindClosestBlockToCarry(aIMovement.GetBlocks()).gameObject;
    }

    public override void OnTriggerEnter(AIMovement aIMovement, Collider2D collider2D)
    {
        
    }

    public override void UpdateState(AIMovement aIMovement)
    {
        if (aIMovement.ReachedDestination())
        {
            aIMovement.PickUpBox(aIMovement.CurrentDestionation);
            aIMovement.SwitchState(aIMovement.PickUpState);
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
        return closestBlock;
    }
}
