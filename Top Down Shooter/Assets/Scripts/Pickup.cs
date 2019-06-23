using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    [SerializeField] protected float lifeSpan = 60f;

    // Start is called before the first frame update
    private void Awake()
    {
        Decay(lifeSpan);
    }

    // Update is called once per frame
    private void Update()
    {
        RotatePickup(30f);
    }

    private void Decay(float timer)
    {
        Destroy(gameObject, timer);
    }

    private void RotatePickup(float rotationAmount)
    {
        transform.Rotate(Vector3.up, rotationAmount * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();


        if (player)
        {
            OnPickUp(player);
        }
    }

    protected virtual void OnPickUp(Player player)
    {
        Destroy(gameObject);
        gameObject.GetComponentInParent<RespawnStationary>().EventRespawn();
    }
}