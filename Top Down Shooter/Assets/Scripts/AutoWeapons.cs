using UnityEngine;

public class AutoWeapons : Weapon
{
    [SerializeField, Tooltip("The maximum angle in degrees the bullets can deviate from the center line\n" +
                             "An angle of 10 means 5 degrees on either side of the center line")]
    private float spreadAngle;

    private float shootTime = 0f;

    private float signedSpreadAngle; // holds half of the spread angle so it can be placed half positive, half negative

    private void Start()
    {
        ammoLeft = ammo;
        signedSpreadAngle = spreadAngle / 2f;
    }

    private void Update()
    {
        if (reloading || !Equipped) return; // no need to do anything for weapons that aren't being used
        ShootInput();
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartReload();
        }
    }

    /// <summary>
    /// Checks for shoot input and determines what the weapon should do
    /// </summary>
    private void ShootInput()
    {
        if (player)
        {
            if (!Input.GetMouseButton(0)) return;
        }

        if (!(Time.time >= shootTime)) return;
        if (ammoLeft > 0)
        {
            Shoot();
        }
        else
        {
            StartReload();
        }
    }

    /// <summary>
    /// Starts a coroutine inherited from the Weapon class, this was just an easy method of adding a reload timer in,
    /// while still allowing everything to operate normally
    /// </summary>
    private void StartReload()
    {
        StartCoroutine(Reload());
    }

    /// <summary>
    /// Overrides the abstract shoot method in the Weapon class. This variant instantiates a bullet and then sets
    /// its damage and origin properties. Then adds force to the bullet, sets a new shoot time, and reduces the
    /// current ammo count
    /// </summary>
    public override void Shoot()
    {
        var bullet = Instantiate(bulletPrefab, barrel.position,
            barrel.rotation * Quaternion.Euler(Random.Range(-signedSpreadAngle, signedSpreadAngle) * Vector3.up));
        var bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.Damage = damage;
        bulletScript.origin = transform;
        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * bulletForce);
        shootTime = Time.time + fireRate;
        ammoLeft--;
    }
}