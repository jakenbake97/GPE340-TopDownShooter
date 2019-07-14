using UnityEngine;

public class AutoWeapons : Weapon
{
    private void Start()
    {
        ammoLeft = ammo;
        signedSpreadAngle = spreadAngle / 2f;
        fireOnClickDown = false;
    }


    /// <summary>
    /// Overrides the abstract shoot method in the Weapon class. This variant instantiates a bullet and then sets
    /// its damage and origin properties. Then adds force to the bullet, sets a new shoot time, and reduces the
    /// current ammo count
    /// </summary>
    protected override void Shoot()
    {
        var bullet = Instantiate(bulletPrefab, barrel.position,
            barrel.rotation * Quaternion.Euler(Random.Range(-signedSpreadAngle, signedSpreadAngle) * Vector3.up));
        var bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.Damage = damage;
        bulletScript.origin = transform;
        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * bulletForce);

        audioSource.PlayOneShot(firingSounds[Random.Range(0, firingSounds.Length)]);

        timeToShoot = Time.time + fireRate;
        ammoLeft--;
    }
}