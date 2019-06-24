using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    [SerializeField, Tooltip("How long this pickup should remain in the scene")]
    protected float lifeSpan = 60f;

    [SerializeField, Tooltip("the speed at which the pickup rotates (degrees per second)")]
    private float rotationSpeed = 30f;

    private void Awake()
    {
        Decay(lifeSpan);
    }

    private void Update()
    {
        RotatePickup(rotationSpeed);
    }

    /// <summary>
    /// counts down the lifespan of the gameobject using the delayed destroy overload
    /// </summary>
    private void Decay(float timer)
    {
        Destroy(gameObject, timer);
    }

    /// <summary>
    /// simply spins the pickup a specified amount, in degrees per second
    /// </summary>
    private void RotatePickup(float rotationAmount)
    {
        transform.Rotate(Vector3.up, rotationAmount * Time.deltaTime);
    }

    /// <summary>
    /// when the player enters the trigger for this object, it calls the virtual pickup method
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();


        if (player)
        {
            OnPickUp(player);
        }
    }

    /// <summary>
    /// sets the basic functionality for pickups, destroys them and says its time to respawn.
    /// additional functionality is overridden in child classes
    /// </summary>
    protected virtual void OnPickUp(Player player)
    {
        Destroy(gameObject);
        gameObject.GetComponentInParent<RespawnStationary>().EventRespawn();
    }
}