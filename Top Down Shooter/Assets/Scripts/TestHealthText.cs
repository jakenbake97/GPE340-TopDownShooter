using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TestHealthText : MonoBehaviour
{
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
        text.text = $"Health: {Mathf.RoundToInt(GameManager.Player.Health.HealthPercent * 100f)}%";
    }
}