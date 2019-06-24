using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TestHealthText : MonoBehaviour
{
    [Tooltip("The player's health component")]
    public Health health;

    private Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
    }

    /// <summary>
    /// Temporary method and class until the official UI gets into the project
    /// basically just uses string interpolation to get the current health percent from the player's health
    /// </summary>
    public void Update()
    {
        text.text = $"Health: {Mathf.RoundToInt(health.HealthPercent * 100f)}%";
    }
}