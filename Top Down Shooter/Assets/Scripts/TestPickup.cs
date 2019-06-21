using UnityEngine;

namespace DefaultNamespace
{
    public class TestPickup : Pickup
    {
        protected override void OnPickUp(InputManager player)
        {
            Debug.LogFormat("I've been picked up by {0}!", player.gameObject.name);
            base.OnPickUp(player);
        }
    }
}