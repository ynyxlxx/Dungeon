using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : LivingEntity {

    public static event System.Action OnDeathStatic;

    protected override void Start () {
        base.Start ();

    }

    public override void TakeHit (float damage, Vector3 hitPoint, Vector3 hitDirection) {

        if (damage >= health) {
            if (OnDeathStatic != null) {
                OnDeathStatic ();
            }

        }
        base.TakeHit (damage, hitPoint, hitDirection);
    }
}