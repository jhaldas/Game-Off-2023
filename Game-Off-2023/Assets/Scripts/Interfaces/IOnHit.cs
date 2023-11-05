using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IOnHit
{
    void OnHealthChange(float damage);
    void OnKnockback(float knockback, Vector2 direction);
}
