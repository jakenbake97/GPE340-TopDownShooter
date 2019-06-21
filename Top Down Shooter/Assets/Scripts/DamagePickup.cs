using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePickup : Pickup
{
    [SerializeField, Tooltip("The amount of damage to apply")]
    private float damage = 10f;

    protected override void OnPickUp(InputManager player)
    {
        var playerHealth = player.gameObject.GetComponent<Health>();
        playerHealth.Damage(damage);
        base.OnPickUp(player);
    }
}