using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable {

    public float startingHealth;
    public float health { get; protected set; }
    protected bool dead;

    public event Action OnDeath;

    protected virtual void Start () {
        health = startingHealth;
    }

    public virtual void TakeHit (float damage, Vector3 hitPoint, Vector3 hitDirection) {
        TakeDamage (damage);
    }

    public virtual void TakeDamage (float damageAmount) {
        health -= damageAmount;

        if (health <= 0 && dead != true) {
            Die ();
        }
    }

    [ContextMenu ("Self Destruct")]
    public virtual void Die () {
        dead = true;
        if (OnDeath != null) {
            OnDeath ();
        }
        GameObject.Destroy (gameObject);
    }
}