using System.Collections;
using UnityEngine;


public abstract class Weapon : MonoBehaviour
{
    [Header("Weapon Settings")] [SerializeField]
    protected float fireRate;

    [SerializeField] protected float damage;

    [SerializeField] protected int ammo;

    [SerializeField] protected float reloadTime;

    [SerializeField] protected float bulletForce;

    protected int ammoLeft;

    protected bool reloading;

    public bool Equipped { get; set; }


    public WeaponAnimationType animationType = WeaponAnimationType.hipFire;

    public enum WeaponAnimationType
    {
        hipFire = 0,
        RifleGrip = 1,
        HandgunGrip = 2
    }

    [SerializeField] protected GameObject bulletPrefab;

    public Transform barrel;

    [Header("IK Settings")] public Transform rightHandIKTarget;

    public Transform leftHandIKTarget;
    public Transform rightElbowIKHint;
    public Transform leftElbowIKHint;

    public abstract void Shoot();

    protected IEnumerator Reload()
    {
        reloading = true;
        ammoLeft = ammo;
        yield return new WaitForSeconds(reloadTime);
        reloading = false;
    }
}