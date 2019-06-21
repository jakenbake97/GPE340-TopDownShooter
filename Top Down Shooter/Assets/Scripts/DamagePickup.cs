using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePickup : Pickup
{
    [SerializeField, Tooltip("The amount of damage to apply")]
    private float damage = 10f;

    protected override void OnPickUp(Player player)
    {
        player.Health.Damage(damage);
        base.OnPickUp(player);
    }
}