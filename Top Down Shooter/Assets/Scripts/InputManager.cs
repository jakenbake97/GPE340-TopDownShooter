﻿using UnityEngine;

public class InputManager : MonoBehaviour
{
    [HideInInspector] public LightFollowsMouse mousePointLight;

    private CharacterAnimationController characterAnimations;

    private Player player;

    //boolean to store a check to see if main camera is not null
    private bool _isMainNotNull;
    private Plane plane;

    private void Awake()
    {
        _isMainNotNull = Camera.main != null; // checks to see if there is a main camera in the scene to prevent from
        // null reference exception, called in awake method to prevent
        // repeated null comparison
        plane = new Plane(Vector3.up, transform.position); //construct a new plane in the awake method to
        //increase performance as opposed to creating a new plane
        //each frame
    }

    private void Start()
    {
        characterAnimations = GetComponent<CharacterAnimationController>();
        player = GetComponent<Player>();
    }

    private void Update()
    {
        if (PauseGameInput()) return;

        AxisInput();
        MouseInputPlaneCast();
        MouseClickInput();
        MiscKeyInput();
    }

    /// <summary>
    /// Checks for pause keyboard input and calls the appropriate GameManager function
    /// </summary>
    private static bool PauseGameInput()
    {
        if (GameManager.Paused)
        {
            if (Input.GetButtonDown("Cancel"))
            {
                GameManager.Resume();
            }

            return true;
        }

        if (!Input.GetButtonDown("Cancel")) return false;
        GameManager.Pause();
        return true;
    }

    /// <summary>
    /// Searches for input on various miscellaneous keys and then tells the player that the key has been pressed
    /// </summary>
    private void MiscKeyInput()
    {
        if (Input.GetButtonDown("Reload"))
        {
            player.reloadInput = true;
        }

        if (Input.GetButtonDown("Slow Mo"))
        {
            player.slowMoInput = true;
        }
    }

    /// <summary>
    /// Searches for mouse button input and then tells the player that the button has been pressed
    /// </summary>
    private void MouseClickInput()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            player.mouseDown = true;
            player.mouseDownTime = 0f;
        }

        if (!Input.GetButtonUp("Fire1")) return;
        player.mouseDown = false;
        player.mouseDownTime = 0f;
    }


    /// <summary>
    /// gets horizontal and vertical axis input and clamps the magnitude to 1, then passes input to
    /// characterAnimationController to move the character using root motion from the blend tree
    /// </summary>
    private void AxisInput()
    {
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        input = Vector3.ClampMagnitude(input, 1f);
        characterAnimations.SetSpeed(input);
    }

    /// <summary>
    /// takes the mouse position and casts a ray from the screen point to the plane created in the awake method,
    /// if the ray hits something then it gets that point in world space and passes it to the
    /// characterAnimationController for rotation adn the LightFollowsMouse to move the light to that point
    /// </summary>
    private void MouseInputPlaneCast()
    {
        if (!_isMainNotNull) return;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!plane.Raycast(ray, out var distance)) return;
        var point = ray.GetPoint(distance);
        characterAnimations.RotateTowardsPoint(point);
        mousePointLight?.LightMovesToPoint(point);
    }
}