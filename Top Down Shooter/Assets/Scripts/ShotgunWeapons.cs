using UnityEngine;

public class ShotgunWeapons : Weapon
{
    [SerializeField, Range(1, 50), Tooltip("The number of bullets that should be fired on each shot")]
    private int bulletsToShoot;

    private void Start()
    {
        ammoLeft = ammo;
        signedSpreadAngle = spreadAngle / 2f;
        fireOnClickDown = true;
    }


    /// <summary>
    /// Overrides the abstract shoot method in the Weapon class. This variant instantiates a bullet and then sets
    /// its damage and origin properties. Then adds force to the bullet, and loops over everything as many times as dictated in bulletsToShoot.
    /// Then sets a new shoot time, and reduces the current ammo count
    /// </summary>
    protected override void Shoot()
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

        audioSource.PlayOneShot(firingSounds[Random.Range(0, firingSounds.Length)]);
        if (muzzleFlashParticle)
        {
            muzzleFlashParticle.Emit(1);
        }

        timeToShoot = Time.time + fireRate;
        ammoLeft--;
    }
}