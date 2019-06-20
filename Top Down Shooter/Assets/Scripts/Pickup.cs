using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private float lifeSpan = 60f;
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

    private void Decay(float lifeSpan)
    {
        Destroy(gameObject, lifeSpan);
    }

    private void RotatePickup(float rotationAmount)
    {
        transform.Rotate(Vector3.up, rotationAmount * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        throw new NotImplementedException();
    }
}