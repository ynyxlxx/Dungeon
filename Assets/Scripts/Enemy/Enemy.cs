using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : LivingEntity {

    public static event System.Action OnDeathStatic;
    public ParticleSystem particleEffect;

    protected override void Start () {
        base.Start ();

    }

    public override void TakeHit (float damage, Vector3 hitPoint, Vector3 hitDirection) {

        if (damage >= health) {
            if (OnDeathStatic != null) {
                OnDeathStatic ();
            }
        }
        AudioManager.Instance.Play ("enemyHit");
        base.TakeHit (damage, hitPoint, hitDirection);
    }

    public override void Die () {
        AudioManager.Instance.Play ("playerDeath");
        ParticleSystem instance = Instantiate (particleEffect, transform.position, Quaternion.identity);
        Destroy (instance, 2f);
        base.Die ();
    }
}