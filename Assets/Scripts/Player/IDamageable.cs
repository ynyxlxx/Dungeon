using UnityEngine;

public interface IDamageable {

    void TakeHit (float Damage, Vector3 hitPoint, Vector3 hitDirection);

    void TakeDamage (float damage);

}