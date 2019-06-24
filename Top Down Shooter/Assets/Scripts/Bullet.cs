using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Damage { get; set; }
    public Transform origin;

    /// <summary>
    /// checks to see if the object that was collided with has a Health component. If so, calls the damage
    /// function on that class and passes in the value stored in the bullet's damage property
    /// </summary>
    private void OnCollisionEnter(Collision other)
    {
        Health health = other.gameObject.GetComponent<Health>();
        if (health)
            health.Damage(Damage);
        Destroy(gameObject);
    }
}