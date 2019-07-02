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

    [SerializeField, Tooltip("The maximum angle in degrees the bullets can deviate from the center line\n" +
                             "An angle of 10 means 5 degrees on either side of the center line")]
    protected float spreadAngle;

    [HideInInspector] public bool fireOnClickDown;

    protected int ammoLeft;

    protected bool reloading;

    protected float
        signedSpreadAngle; // holds half of the spread angle so it can be placed half positive, half negative

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

    [Header("AI Settings"), Range(1f, 180f)]
    public float attackAngle = 10f;

    public float maxRange = 20f;

    [Header("IK Settings")] [Tooltip("The Transform component to represent the target for IK")]
    public Transform rightHandIKTarget;

    [Tooltip("The Transform component to represent the target for IK")]
    public Transform leftHandIKTarget;

    [Tooltip("The Transform component to represent the target for IK")]
    public Transform rightElbowIKHint;

    [Tooltip("The Transform component to represent the target for IK")]
    public Transform leftElbowIKHint;

    [HideInInspector] public bool player = false;
    protected float timeToShoot = 0f;

    /// <summary>
    /// abstract method that must be overridden in child classes
    /// </summary>
    protected abstract void Shoot();

    /// <summary>
    /// coroutine for reloading the weapon, resets the ammo and waits a specified amount of time before allowing
    /// the weapon to be used again
    /// </summary>
    private IEnumerator Reload()
    {
        reloading = true;
        ammoLeft = ammo;
        yield return new WaitForSeconds(reloadTime);
        reloading = false;
    }

    /// <summary>
    /// Called from weaponAgents, this determines if the weapon is allowed to fire or not
    /// </summary>
    public void processShoot()
    {
        if (reloading) return;

        if (Time.time >= timeToShoot && ammoLeft > 0)
        {
            Shoot();
        }
        else if (ammoLeft == 0)
        {
            StartReload();
        }
    }

    /// <summary>
    /// Starts a coroutine inherited from the Weapon class, this was just an easy method of adding a reload timer in,
    /// while still allowing everything to operate normally
    /// </summary>
    public void StartReload()
    {
        StartCoroutine(Reload());
    }
}