﻿using UnityEngine;

public class LightFollowsMouse : MonoBehaviour
{
    [SerializeField, Tooltip("The height above the ground the light should follow the mouse at")]
    private float lightHeight = 2f;

    /// <summary>
    /// Takes a target point from the mouse position in the input manager and moves the light this script is attached to,
    /// to that point + the lightHeight offset in the y axis
    /// </summary>
    public void LightMovesToPoint(Vector3 targetPoint)
    {
        transform.position = new Vector3(targetPoint.x, targetPoint.y + lightHeight, targetPoint.z);
    }

    private void Update()
    {
        if (GameManager.Paused) return;
        if (!GameManager.Player.InputManager.mousePointLight)
        {
            GameManager.Player.InputManager.mousePointLight = this;
        }
    }
}