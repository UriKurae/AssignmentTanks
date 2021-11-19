using UnityEngine;

using Pada1.BBCore;
using Pada1.BBCore.Framework;

[Condition("MyConditions/IsTankFar?")]
[Help("Checks if the enemy tank is far.")]
public class IsTankFar : ConditionBase
{
    [InParam("Self tank")]
    [Help("This is the tank of this behaviour tree")]
    public GameObject selfTank;

    [InParam("Enemy tank")]
    [Help("The enemy tank of this behaviour tree")]
    public GameObject enemyTank;


    public Blackboard blackboard;
    
    //public override bool Check()
    //{
    //    Debug.Log("HOLA");
    //    return (Vector3.Distance(selfTank.transform.position, enemyTank.transform.position) > 5.0f);
    //}
    public override bool Check()
    {
        Debug.Log("HOLA");
        return true;
    }
}
