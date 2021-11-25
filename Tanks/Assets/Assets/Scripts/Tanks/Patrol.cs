using UnityEngine;
using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Tasks;     // TaskStatus
using Pada1.BBCore.Framework; // BasePrimitiveAction
using UnityEngine.AI;

[Action("MyActions/Patrol")]
[Help("Get the tank to patrol")]
public class Patrol : BasePrimitiveAction
{
    [InParam("Self tank")]
    [Help("Tank of this behaviour Tree")]
    public GameObject selfTank;

    public override TaskStatus OnUpdate()
    {
        NavMeshAgent agent = selfTank.GetComponent<NavMeshAgent>();
        Moves moves = selfTank.GetComponent<Moves>();
        moves.TurnAround();
        if (!agent.pathPending && agent.remainingDistance < 0.5f) moves.Patrol();

        return TaskStatus.COMPLETED;
    }
}