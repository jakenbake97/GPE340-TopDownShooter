using UnityEngine;

public class HealPickup : Pickup
{
    [SerializeField, Tooltip("The amount to heal")]
    private float heal = 10f;

    /// <summary>
    /// Overrides the OnPickUp method in the pickup class and calls the heal function on the player's health component
    /// </summary>
    protected override void OnPickUp(Player player)
    {
        player.Health.Heal(heal);
        base.OnPickUp(player);
    }
}