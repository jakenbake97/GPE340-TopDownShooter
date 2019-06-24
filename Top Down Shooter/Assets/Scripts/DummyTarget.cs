using UnityEngine;

public class DummyTarget : MonoBehaviour
{
    private Transform bulletParent;

    /// <summary>
    /// on collision, checks to see if the other object was a bullet and if so gets a reference to the
    /// Transform in the origin field
    /// </summary>
    private void OnCollisionEnter(Collision other)
    {
        var bullet = other.gameObject.GetComponent<Bullet>();
        if (bullet)
        {
            bulletParent = bullet.origin;
        }
    }

    /// <summary>
    /// Called from the "onDamage" event by the health script on the target, takes the transform from the bullet and
    /// rotates the GameObject to face it, used just as a simple way to show that the bullet hit the target
    /// </summary>
    public void LookAtShooter()
    {
        transform.LookAt(bulletParent);
    }

    /// <summary>
    /// Called from the "onDie" event on the health script of the target, this calls the EventRespawn method of the
    /// spawner.
    /// </summary>
    public void ParentEventRespawn()
    {
        GetComponentInParent<RespawnStationary>().EventRespawn();
    }
}