using UnityEngine;
using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Tasks;     // TaskStatus
using Pada1.BBCore.Framework; // BasePrimitiveAction

[Action("MyActions/RunToReload")]
[Help("Get the tank roaming")]
public class RunToReload : BasePrimitiveAction
{
    [InParam("Self tank")]
    [Help("Tank of this behaviour Tree")]
    public GameObject selfTank;

    [InParam("Base to Reload")]
    [Help("Base to where this tank has to reload")]
    public GameObject baseToReload;

    public override TaskStatus OnUpdate()
    {
        Moves moves = selfTank.GetComponent<Moves>();
        moves.RunToBase(baseToReload.transform.position);
        return TaskStatus.COMPLETED;
    }
}