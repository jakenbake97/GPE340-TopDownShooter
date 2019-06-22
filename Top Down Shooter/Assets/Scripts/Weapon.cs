using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Settings"), SerializeField]
    private float fireRate;

    public WeaponAnimationType animationType = WeaponAnimationType.hipFire;

    public enum WeaponAnimationType
    {
        hipFire = 0,
        RifleGrip = 1,
        HandgunGrip = 2
    }

    [Header("IK Settings")] public Transform rightHandIKTarget;

    public Transform leftHandIKTarget;
}