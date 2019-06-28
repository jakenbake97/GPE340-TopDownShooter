using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Animator), typeof(CapsuleCollider))]
[RequireComponent(typeof(Health))]
public class WeaponAgent : MonoBehaviour
{
    private Health health;

    private Animator anim;

    [SerializeField] protected GameObject equippedWeapon;

    private Weapon currentWeapon;

    [SerializeField] protected Transform attachmentPoint;

    public virtual void Awake()
    {
        health = GetComponent<Health>();
        anim = GetComponent<Animator>();
        currentWeapon = equippedWeapon.GetComponent<Weapon>();
    }
}