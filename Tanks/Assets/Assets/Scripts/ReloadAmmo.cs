using UnityEngine;
using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Tasks;     // TaskStatus
using Pada1.BBCore.Framework; // BasePrimitiveAction

[Action("MyActions/ReloadAmmo")]
[Help("Reload max ammo for this tank")]
public class ReloadAmmo : BasePrimitiveAction
{
    [InParam("Self tank")]
    [Help("Tank of this behaviour Tree")]
    public GameObject selfTank;

    public override TaskStatus OnUpdate()
    {
        Moves moves = selfTank.GetComponent<Moves>();
        moves.Reload();
        return TaskStatus.COMPLETED;
    }
}