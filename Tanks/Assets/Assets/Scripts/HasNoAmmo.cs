using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pada1.BBCore;
using Pada1.BBCore.Framework;

[Condition("MyConditions/HasAmmo?")]
[Help("Checks if this tank has ammo.")]
public class HasNoAmmo : ConditionBase
{
    [InParam("Ammo left")]
    [Help("Ammo left of the tank")]
    public Moves moves;

    public override bool Check()
    {
        return moves.ammo < 0;
    }
}
