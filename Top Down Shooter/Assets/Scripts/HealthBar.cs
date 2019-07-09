using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Text text;

    [SerializeField] private string textFormat;

    [SerializeField] private Image fill;

    [SerializeField] private bool trackTarget;

    [SerializeField] private bool destroyWithTarget;

    [SerializeField] private Vector3 trackingOffset;


    private Health target;

    public void SetTarget(Health value)
    {
        target = value;
        if (destroyWithTarget)
        {
            value.onDie.AddListener(HandleTargetDeath);
        }
    }

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