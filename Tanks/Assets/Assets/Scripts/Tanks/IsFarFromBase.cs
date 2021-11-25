using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pada1.BBCore;
using Pada1.BBCore.Framework;

[Condition("MyConditions/IsFarFromBase?")]
[Help("Checks if this behaviour's brick tank is far from the reload base.")]
public class IsFarFromBase : ConditionBase
{
    [InParam("Self tank")]
    [Help("This is the tank of this behaviour tree")]
    public GameObject selfTank;

    [InParam("Base location")]
    [Help("Location of the base")]
    public GameObject baseLocation;

    public override bool Check()
    {
        return (Vector3.Distance(selfTank.transform.position, baseLocation.transform.position) > 2.0f);
    }
}
