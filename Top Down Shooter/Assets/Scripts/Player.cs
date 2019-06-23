using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Health), typeof(CharacterAnimationController), typeof(InputManager))]
public class Player : MonoBehaviour
{
    public Health Health { get; private set; }
    private CharacterAnimationController charAnimController;

    [Header("Weapon Settings"), SerializeField, Tooltip("The prefab for the default weapon to be equipped")]
    private GameObject weaponPrefab;

    [SerializeField, Tooltip("The position the weapon should be spawned")]
    private Transform attachmentPoint;

    private GameObject equippedWeapon;
    private Weapon currentWeapon;

    private void Awake()
    {
        Health = GetComponent<Health>();
        charAnimController = GetComponent<CharacterAnimationController>();
    }

    // Update is called once per frame
    private void Start()
    {
        EquipWeapon(weaponPrefab);
    }

    public void EquipWeapon(GameObject prefab)
    {
        equippedWeapon = Instantiate(prefab, attachmentPoint, false);
        //Get weapon offset from weapon itself
        currentWeapon = equippedWeapon.GetComponent<Weapon>();
        currentWeapon.Equipped = true;
        charAnimController.SetWeaponAnimationProperties((int) currentWeapon.animationType);
    }

    public void UnequipWeapon()
    {
        if (!equippedWeapon) return;
        currentWeapon.Equipped = false;
        Destroy(equippedWeapon);
        equippedWeapon = null;
        currentWeapon = null;
    }

    protected void OnAnimatorIK(int layerIndex)
    {
        if (!equippedWeapon || !currentWeapon) return;
        charAnimController.SetCharacterIKAnimation(currentWeapon.rightHandIKTarget, currentWeapon.leftHandIKTarget);
    }
}