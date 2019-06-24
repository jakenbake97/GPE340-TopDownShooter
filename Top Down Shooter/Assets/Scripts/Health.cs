using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [Header("Health Stats"), SerializeField, Tooltip("The maximum health allowed")]
    private float maxHealth = 200f;

    [SerializeField, Tooltip("The starting health value")]
    private float initialHealth = 200f;

    public float HealthValue { get; private set; }

    public float HealthPercent => HealthValue / maxHealth; //shorthand setter

    [Header("Events"), SerializeField, Tooltip("Activated every time the object is healed")]
    private UnityEvent onHeal;

    [SerializeField, Tooltip("Activated every time the object is damaged")]
    private UnityEvent onDamage;

    [SerializeField, Tooltip("Activated once when the object's healthValue reaches 0")]
    private UnityEvent onDie;

    private void Awake()
    {
        HealthValue = initialHealth;
    }

    /// <summary>
    /// called by external sources when something wants to damage this gameobject. First checks to make sure
    /// the HealthValue isn't already 0, preventing from dieing multiple times in the same frame. then takes the
    /// absolute value of the damage, preventing it from being negative and subtracts from the the HealthValue.
    /// Next, Invokes the onDamage event, and if the object will die, calls the Die method
    /// </summary>
    public void Damage(float damage)
    {
        if (HealthValue <= 0) return;
        if (damage < 0) //damage must be positive
        {
            damage *= -1;
        }

        HealthValue -= damage;
        onDamage.Invoke();
        if (!(HealthValue <= 0f)) return;
        onDie.Invoke();
        Die();
    }

    /// <summary>
    /// called by external sources when something wants to heal this gameobject. First takes the absolute value of
    /// the heal, preventing it from being negative, and adds it to a clamped HealthValue to prevent over healing.
    /// Then Invokes the onHeal event
    /// </summary>
    public void Heal(float heal)
    {
        if (heal < 0) //heal must be positive
        {
            heal *= -1;
        }

        HealthValue = Mathf.Clamp(HealthValue + heal, 0f, maxHealth);
        onHeal.Invoke();
    }

    /// <summary>
    /// simply destroys the gameobject when health reaches 0
    /// </summary>
    private void Die()
    {
        Destroy(gameObject);
    }
}