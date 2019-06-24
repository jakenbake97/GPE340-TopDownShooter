using UnityEngine;

public class WeaponPickup : Pickup
{
    [SerializeField, Tooltip("The Weapon To equip from this pickup")]
    private GameObject weapon;

    /// <summary>
    /// overrides the OnPickUp method of the pickup class and tells the player to unequip the current weapon and
    /// equip the one this pickup represents
    /// </summary>
    protected override void OnPickUp(Player player)
    {
        player.UnequipWeapon();
        player.EquipWeapon(weapon);
        base.OnPickUp(player);
    }
}