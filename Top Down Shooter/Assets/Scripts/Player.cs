using UnityEngine;

[RequireComponent(typeof(CharacterAnimationController), typeof(InputManager))]
public class Player : WeaponAgent
{
    private CharacterAnimationController charAnimController;


    public override void Awake()
    {
        charAnimController = GetComponent<CharacterAnimationController>();
        base.Awake();
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