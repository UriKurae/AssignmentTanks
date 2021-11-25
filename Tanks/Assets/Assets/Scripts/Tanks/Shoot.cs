using UnityEngine;
using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Tasks;     // TaskStatus
using Pada1.BBCore.Framework; // BasePrimitiveAction

[Action("MyActions/Shoot")]
[Help("Get the tank roaming")]
public class Shoot : BasePrimitiveAction
{
    [InParam("Self tank")]
    [Help("Tank of this behaviour Tree")]
    public GameObject selfTank;

    public override TaskStatus OnUpdate()
    {
        Moves moves = selfTank.GetComponent<Moves>();
        moves.Shoot();
        return TaskStatus.COMPLETED;
    }
}