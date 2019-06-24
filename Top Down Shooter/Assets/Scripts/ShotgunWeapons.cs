using UnityEngine;

public class ShotgunWeapons : Weapon
{
    [SerializeField, Range(1, 50), Tooltip("The number of bullets that should be fired on each shot")]
    private int bulletsToShoot;

    [SerializeField, Tooltip("The maximum angle in degrees the bullets can deviate from the center line\n" +
                             "An angle of 10 means 5 degrees on either side of the center line")]
    private float spreadAngle;

    private float shootTime = 0f;
    private float signedSpreadAngle;

    private void Start()
    {
        ammoLeft = ammo;
        signedSpreadAngle = spreadAngle / 2f;
    }

    private void Update()
    {
        if (reloading || !Equipped) return;
        ShootInput();
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartReload();
        }
    }

    /// <summary>
    /// Checks for appropriate shoot input and determines what the weapon should do
    /// </summary>
    private void ShootInput()
    {
        if (!Input.GetMouseButtonDown(0)) return;
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
    /// its damage and origin properties. Then adds force to the bullet, and loops over everything as many times as dictated in bulletsToShoot.
    /// Then sets a new shoot time, and reduces the current ammo count
    /// </summary>
    public override void Shoot()
    {
        for (int i = 0; i < bulletsToShoot; i++)
        {
            var bullet = Instantiate(bulletPrefab, barrel.position,
                barrel.rotation * Quaternion.Euler(Random.Range(-signedSpreadAngle, signedSpreadAngle) * Vector3.up));
            var bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.Damage = damage;
            bulletScript.origin = transform;
            bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * bulletForce);
        }

        shootTime = Time.time + fireRate;
        ammoLeft--;
    }
}