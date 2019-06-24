using UnityEngine;

public class DamagePickup : Pickup
{
    [SerializeField, Tooltip("The amount of damage to apply")]
    private float damage = 10f;

    /// <summary>
    /// Overrides the OnPickUp method of the pickup class and calls the damage function on the player's
    /// health component
    /// </summary>
    protected override void OnPickUp(Player player)
    {
        player.Health.Damage(damage);
        base.OnPickUp(player);
    }
}