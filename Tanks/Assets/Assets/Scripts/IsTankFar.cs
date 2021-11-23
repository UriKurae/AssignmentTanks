using UnityEngine;

using Pada1.BBCore;
using Pada1.BBCore.Framework;

[Condition("MyConditions/IsTankFar?")]
[Help("Checks if the enemy tank is far, true if it's far.")]
public class IsTankFar : ConditionBase
{
    [InParam("Self Tank")]
    [Help("This is the tank of this behaviour tree")]
    public GameObject selfTank;

    [InParam("Enemy Tank")]
    [Help("The enemy tank of this behaviour tree")]
    public GameObject enemyTank;

    public override bool Check()
    {
        return Vector3.Distance(selfTank.transform.position, enemyTank.transform.position) > 15.0f;
    }
}