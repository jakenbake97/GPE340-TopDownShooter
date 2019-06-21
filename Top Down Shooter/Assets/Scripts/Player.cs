using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health), typeof(CharacterAnimationController), typeof(InputManager))]
public class Player : MonoBehaviour
{
    public Health Health { get; private set; }
    private CharacterAnimationController charAnimController;
    
    //TODO: add references to the other components a player needs to operate

    
    private void Awake()
    {
        Health = GetComponent<Health>();
        charAnimController = GetComponent<CharacterAnimationController>();
    }

    // Update is called once per frame
    void Update()
    {
    }
}