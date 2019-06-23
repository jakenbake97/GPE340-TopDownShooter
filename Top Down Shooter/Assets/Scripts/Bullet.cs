using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Damage { get; set; }

    private void OnCollisionEnter(Collision other)
    {
        Health health = other.gameObject.GetComponent<Health>();
        if (health)
            health.Damage(Damage);
        Destroy(gameObject);
    }
}