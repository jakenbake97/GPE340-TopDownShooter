using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Damage { get; set; }
    [HideInInspector] public Transform origin;

    [SerializeField] private ParticleSystem dustParticle;

    [SerializeField] private ParticleSystem bloodParticle;

    /// <summary>
    /// checks to see if the object that was collided with has a Health component. If so, calls the damage
    /// function on that class and passes in the value stored in the bullet's damage property
    /// </summary>
    private void OnCollisionEnter(Collision other)
    {
        Health health = other.gameObject.GetComponent<Health>();
        ParticleSystem particle;
        if (health)
        {
            health.Damage(Damage);
            var rotation = transform.rotation;
            particle = Instantiate(bloodParticle, other.contacts[0].point,
                new Quaternion(rotation.x, rotation.y - 180f, rotation.z, rotation.w));
        }
        else
        {
            var rotation = transform.rotation;
            particle = Instantiate(dustParticle, other.contacts[0].point,
                new Quaternion(rotation.x, rotation.y - 180f, rotation.z, rotation.w));
        }

        Destroy(particle.gameObject, particle.main.duration + particle.main.startLifetime.Evaluate(1));
        Destroy(gameObject);
    }
}