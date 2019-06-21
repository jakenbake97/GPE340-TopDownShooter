using UnityEngine;

public class TestPickup : Pickup
{
    protected override void OnPickUp(Player player)
    {
        Debug.LogFormat("I've been picked up by {0}!", player.gameObject.name);
        base.OnPickUp(player);
    }
}