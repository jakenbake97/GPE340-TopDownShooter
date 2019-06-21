using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPickup : Pickup
{
    [SerializeField, Tooltip("The amount to heal")]
    private float heal = 10f;

    protected override void OnPickUp(Player player)
    {
        player.Health.Heal(heal);
        base.OnPickUp(player);
    }
}