using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public float slowDownFactor = 0.25f;
    public float slowDownLength = 5f;
    public float abilityCoolDown = 10f;
    private float timeMultiplier;
    private float nextUse = 0f;
    private bool slowMo = false;
    private float exitTime;
    private float originalFixedTime;
    private CharacterAnimationController characterAnimationController;
    private float originalRotation;

    public void DoSlowMotion(CharacterAnimationController animatorController)
    {
        if (!(Time.time >= nextUse)) return;
        Time.timeScale = slowDownFactor;
        timeMultiplier = 1 / slowDownFactor;
        characterAnimationController = animatorController;
        originalRotation = animatorController.rotationSpeed;
        animatorController.rotationSpeed = originalRotation * timeMultiplier;
        characterAnimationController.anim.speed = timeMultiplier;
        originalFixedTime = Time.fixedDeltaTime;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
        exitTime = Time.unscaledTime + slowDownLength;
        slowMo = true;
    }

    private void Update()
    {
        if (!slowMo) return;
        if (!(exitTime <= Time.unscaledTime)) return;
        Time.timeScale = 1;
        characterAnimationController.anim.speed = 1f;
        characterAnimationController.rotationSpeed = originalRotation;
        Time.fixedDeltaTime = originalFixedTime;
        nextUse = Time.time + abilityCoolDown;
        slowMo = false;
    }
}