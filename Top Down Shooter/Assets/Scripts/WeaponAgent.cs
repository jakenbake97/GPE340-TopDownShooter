using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Animator), typeof(CapsuleCollider))]
[RequireComponent(typeof(Health))]
public class WeaponAgent : MonoBehaviour
{
    public Health Health { get; private set; }

    protected Animator anim;


    protected GameObject equippedWeapon;

    protected Weapon currentWeapon;

    [SerializeField, Tooltip("The position the weapon should be spawned")]
    protected Transform attachmentPoint;

    public virtual void Awake()
    {
        Health = GetComponent<Health>();
        anim = GetComponent<Animator>();
        currentWeapon = equippedWeapon.GetComponent<Weapon>();
    }

    /// <summary>
    /// when an item is picked up, EquipWeapon is called. A weapon is instantiated into the character's hands
    /// and we cache a reference to the class that inherits from weapon on this object.
    /// </summary>
    public virtual void EquipWeapon(GameObject prefab)
    {
        equippedWeapon = Instantiate(prefab, attachmentPoint, false);
        //Get weapon offset from weapon itself
        currentWeapon = equippedWeapon.GetComponent<Weapon>();
        currentWeapon.Equipped = true;
    }

    /// <summary>
    /// When a new weapon is picked up we need to get rid of the old one, to this destroys the current weapon and
    /// preps all the variables for reassignment
    /// </summary>
    public void UnequipWeapon()
    {
        if (!equippedWeapon) return;
        currentWeapon.Equipped = false;
        Destroy(equippedWeapon);
        equippedWeapon = null;
        currentWeapon = null;
    }
}