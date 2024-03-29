﻿using UnityEngine;

[RequireComponent(typeof(CharacterAnimationController), typeof(InputManager))]
public class Player : WeaponAgent
{
    [Header("Weapon Settings"), SerializeField, Tooltip("The prefab for the default weapon to be equipped")]
    private GameObject weaponPrefab;

    private CharacterAnimationController charAnimController;

    [HideInInspector] public bool mouseDown = false;

    [HideInInspector] public float mouseDownTime;

    [HideInInspector] public bool reloadInput = false;
    [HideInInspector] public bool slowMoInput = false;
    public InputManager InputManager { get; private set; }
    private TimeController timeController;

    public override void Awake()
    {
        InputManager = GetComponent<InputManager>();
        charAnimController = GetComponent<CharacterAnimationController>();
        EquipWeapon(weaponPrefab);
        timeController = GetComponent<TimeController>();
        base.Awake();
        UIManager.Instance.RegisterPlayer(this);
    }

    private void Update()
    {
        if (GameManager.Paused) return;
        if (slowMoInput)
        {
            timeController.DoSlowMotion(charAnimController);
            slowMoInput = false;
        }

        if (reloadInput)
        {
            currentWeapon.StartReload();
            reloadInput = false;
        }

        MouseDownWeaponControl();
    }

    /// <summary>
    /// If the fire button is being pressed, determine if the weapon is semi or automatic and shoot if the mouse
    /// down time is appropriate
    /// </summary>
    private void MouseDownWeaponControl()
    {
        if (!mouseDown) return;
        if (!currentWeapon) return;


        if (currentWeapon.fireOnClickDown)
        {
            if (mouseDownTime <= Time.deltaTime)
            {
                currentWeapon.processShoot();
            }
        }
        else
        {
            currentWeapon.processShoot();
        }


        mouseDownTime += Time.deltaTime;
    }

    /// <summary>
    /// when an item is picked up, EquipWeapon is called. A weapon is instantiated into the character's hands
    /// and we cache a reference to the class that inherits from weapon on this object. it is then told that is has
    /// been equipped and the animation controller is told of it's animation type.
    /// </summary>
    public override void EquipWeapon(GameObject prefab)
    {
        base.EquipWeapon(prefab);
        charAnimController.SetWeaponAnimationProperties((int) currentWeapon.animationType);
        currentWeapon.player = true;
    }

    /// <summary>
    /// when the animator does an IK pass, hand over all of the IK targets from the current weapon to the
    /// characterAnimationController
    /// </summary>
    protected void OnAnimatorIK(int layerIndex)
    {
        if (!equippedWeapon || !currentWeapon) return;
        charAnimController.SetCharacterIKAnimation(currentWeapon.rightHandIKTarget, currentWeapon.leftHandIKTarget,
            currentWeapon.rightElbowIKHint, currentWeapon.leftElbowIKHint);
    }
}