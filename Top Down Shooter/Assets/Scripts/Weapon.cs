using System.Collections;
using UnityEngine;


public abstract class Weapon : MonoBehaviour
{
    [Header("Weapon Settings")] [SerializeField, Tooltip("The amount of time (in seconds) between each shot")]
    protected float fireRate;

    [SerializeField, Tooltip("The amount of damage each bullet does")]
    protected float damage;

    [SerializeField, Tooltip("the amount of ammo that is considered a full clip")]
    protected int ammo;

    [SerializeField, Tooltip("The amount of time (in seconds) it takes to reload the weapon")]
    protected float reloadTime;

    [SerializeField, Tooltip("The amount of force to add to the bullet's rigidbody after spawning")]
    protected float bulletForce;

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

    [SerializeField, Tooltip("Prefab for the object to be used as the bullet")]
    protected GameObject bulletPrefab;

    public Transform barrel;

    [Header("IK Settings")] [Tooltip("The Transform component to represent the target for IK")]
    public Transform rightHandIKTarget;

    [Tooltip("The Transform component to represent the target for IK")]
    public Transform leftHandIKTarget;

    [Tooltip("The Transform component to represent the target for IK")]
    public Transform rightElbowIKHint;

    [Tooltip("The Transform component to represent the target for IK")]
    public Transform leftElbowIKHint;

    /// <summary>
    /// abstract method that must be overridden in child classes
    /// </summary>
    public abstract void Shoot();

    /// <summary>
    /// coroutine for reloading the weapon, resets the ammo and waits a specified amount of time before allowing
    /// the weapon to be used again
    /// </summary>
    protected IEnumerator Reload()
    {
        reloading = true;
        ammoLeft = ammo;
        yield return new WaitForSeconds(reloadTime);
        reloading = false;
    }
}