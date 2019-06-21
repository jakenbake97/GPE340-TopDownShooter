using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TestHealthText : MonoBehaviour
{
    public Health health;
    private Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    public void Update()
    {
        text.text = $"Health: {Mathf.RoundToInt(health.HealthPercent * 100f)}%";
    }
}