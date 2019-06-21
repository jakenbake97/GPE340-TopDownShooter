using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    //variable max and initial health
    //ability to keep track of health and have other scripts view that value
    //ability to take damage from things
    //ability to heal
    //notify other objects of state changes when taking damage or when destroyed
    // Start is called before the first frame update
    [Header("Health Stats"), SerializeField, Tooltip("The maximum health allowed")]
    private float maxHealth = 200f;

    [SerializeField, Tooltip("The starting health value")]
    private float initialHealth = 200f;

    public float HealthValue { get; private set; }

    public float HealthPercent => HealthValue / maxHealth;

    [Header("Events"), SerializeField, Tooltip("Activated every time the object is healed")]
    private UnityEvent onHeal;

    [SerializeField, Tooltip("Activated every time the object is damaged")]
    private UnityEvent onDamage;

    [SerializeField, Tooltip("Activated once when the object's healthValue reaches 0")]
    private UnityEvent onDie;

    public void Damage(float damage)
    {
        if (damage < 0) //damage must be positive
        {
            damage *= -1;
        }

        HealthValue -= damage;
        onDamage.Invoke();
        if (HealthValue <= 0f)
        {
            onDie.Invoke();
        }
    }

    public void Heal(float heal)
    {
        if (heal < 0) //heal must be positive
        {
            heal *= -1;
        }

        HealthValue = Mathf.Clamp(HealthValue + heal, 0f, maxHealth);
        onHeal.Invoke();
    }
}