﻿using DG.Tweening;
using UnityEngine;

public class CollisionScanProjectile : Projectile
{
    protected override void OnTargetCollision(Collision2D collision2D, IDamageable damageable)
    {
        int damagableLayer = LayerMask.NameToLayer("Damageable");
        int hitableLayer = LayerMask.NameToLayer("Hitable");

        if (collision2D.gameObject.layer == damagableLayer || collision2D.gameObject.layer == hitableLayer)
        {
            damageable.ApplyDamage(Damage);
            Debug.Log(gameObject.name + " наносит ему - " + collision2D.gameObject.name + " урон = " + Damage);
        }
    }
}
