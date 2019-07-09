using UnityEngine;

public class TimeController : MonoBehaviour
{
    [SerializeField, Tooltip("The value timescale should be changed to\n " +
                             "for example, 0.5 maps 1/2 second in game to 1 second realtime")]
    private float slowDownFactor = 0.25f;

    [SerializeField, Tooltip("The amount of time in realtime seconds the slowdown should be applied")]
    private float slowDownLength = 5f;

    [SerializeField, Tooltip("The amount of time after resuming realtime time scale that this ability should cooldown")]
    private float abilityCoolDown = 10f;

    private float timeMultiplier;
    private float nextUse = 0f;
    private bool slowMo = false;
    private float exitTime;
    private float originalFixedTime;
    private CharacterAnimationController characterAnimationController;
    private float originalRotation;

    /// <summary>
    /// Called from the Player Class, this sets the timescale to be slowed down and speeds up the animator so the
    /// player moves at the same speed relative to the world in realtime
    /// </summary>
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
        exitTime = Time.time + (slowDownLength * slowDownFactor);
        slowMo = true;
    }

    private void Update()
    {
        if (GameManager.Paused) return;
        if (!slowMo) return;
        ResumeRealTime();
    }


    /// <summary>
    /// Converts time back to realtime and slows the animator back down to normal speed
    /// </summary>
    private void ResumeRealTime()
    {
        if (!(exitTime <= Time.time)) return;
        Time.timeScale = 1;
        characterAnimationController.anim.speed = 1f;
        characterAnimationController.rotationSpeed = originalRotation;
        Time.fixedDeltaTime = originalFixedTime;
        nextUse = Time.time + abilityCoolDown;
        slowMo = false;
    }
}