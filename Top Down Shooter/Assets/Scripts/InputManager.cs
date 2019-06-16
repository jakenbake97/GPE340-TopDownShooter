﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private CharacterAnimationController characterAnimations;

    // Start is called before the first frame update
    private void Start()
    {
        characterAnimations = GetComponent<CharacterAnimationController>();
    }

    // Update is called once per frame
    private void Update()
    {
        AxisInput();
    }

    private void AxisInput()
    {
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        input = Vector3.ClampMagnitude(input, 1f);
        characterAnimations.SetSpeed(input);
    }
}