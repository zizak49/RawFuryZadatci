
using UnityEngine;
public abstract class State
{
    public abstract void EnterState(AIMovement aIMovement);
    public abstract void UpdateState(AIMovement aIMovement);
    public abstract void OnTriggerEnter(AIMovement aIMovement, Collider2D collider2D);
}
