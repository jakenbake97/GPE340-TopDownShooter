using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField, Tooltip("The text displaying on the health bar")]
    private Text text;

    [SerializeField, Tooltip("The formatting to apply to the health bar text")]
    private string textFormat;

    [SerializeField, Tooltip("The image to use to fill in the health bar")]
    private Image fill;

    [SerializeField, Tooltip("Should the health bar be stationary of track the health target?")]
    private bool trackTarget;

    [SerializeField, Tooltip("Should the health bar be destroyed when the target dies?")]
    private bool destroyWithTarget;

    [SerializeField, Tooltip("The amount to offset the position of the health bar from the tracked target's position")]
    private Vector3 trackingOffset;

    private Health target;

    /// <summary>
    /// Called by GameManager when a new player or enemy is spawned to have their health bar track health
    /// </summary>
    public void SetTarget(Health value)
    {
        target = value;
        if (destroyWithTarget)
        {
            value.onDie.AddListener(HandleTargetDeath);
        }
    }

    /// <summary>
    /// called by the onDie event from the listener added when target was set. Removes listener and destroys health bar
    /// </summary>
    private void HandleTargetDeath()
    {
        target.onDie.RemoveListener(HandleTargetDeath);
        Destroy(gameObject);
    }


    private void Update()
    {
        if (!target) return;

        if (text)
            text.text = string.Format(textFormat, target.HealthValue);

        fill.fillAmount = target.HealthPercent;

        if (trackTarget)
        {
            transform.position = target.transform.position + trackingOffset;
        }
    }
}