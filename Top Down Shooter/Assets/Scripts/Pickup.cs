using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    private const float lifeSpan = 60f;

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
        //TODO: make player hold a reference to the player GameObject instead of a script on the player and search for
        //PlayerData class.

        InputManager player = other.GetComponent<InputManager>(); //Looking for inputManager, since Player class
        //became CharacterAnimationController and could be 
        //used by NPC


        if (player)
        {
            OnPickUp(player);
        }
    }

    protected virtual void OnPickUp(InputManager player)
    {
        Destroy(gameObject);
    }
}