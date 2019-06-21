using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : Pickup
{
    [SerializeField, Tooltip("The Weapon To equip from this pickup")]
    private GameObject weapon;

    protected override void OnPickUp(Player player)
    {
        player.UnequipWeapon();
        player.EquipWeapon(weapon);
        base.OnPickUp(player);
    }
}