using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pada1.BBCore;
using Pada1.BBCore.Framework;

[Condition("MyConditions/IsReadyToShoot?")]
[Help("Checks if this tank has ammo and can shoot.")]
public class HasAmmo : ConditionBase
{
    [InParam("Ammo left")]
    [Help("Ammo left of the tank")]
    public Moves moves;

    [InParam("Enemy Tank")]
    [Help("Enemy tank")]
    public GameObject enemyTank;

    [InParam("self Tank")]
    [Help("self tank")]
    public GameObject selfTank;

    public override bool Check()
    {
        if (selfTank && enemyTank)
            return ((moves.ammo > 0) && (Vector3.Distance(selfTank.transform.position, enemyTank.transform.position) < 15.0f));
        else
            return false;
    }
}
