using UnityEngine;


public abstract class Weapon : MonoBehaviour
{
    [Header("Weapon Settings")] [SerializeField]
    private float fireRate;

    [SerializeField] private float damage;

    [SerializeField] private int ammo;

    [SerializeField] private float reloadTime;

    public WeaponAnimationType animationType = WeaponAnimationType.hipFire;

    public enum WeaponAnimationType
    {
        hipFire = 0,
        RifleGrip = 1,
        HandgunGrip = 2
    }

    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private Transform barrel;

    [Header("IK Settings")] public Transform rightHandIKTarget;

    public Transform leftHandIKTarget;

    public abstract void Shoot();
}