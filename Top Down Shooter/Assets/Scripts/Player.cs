using UnityEngine;

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

    private void Start()
    {
        EquipWeapon(weaponPrefab);
    }

    /// <summary>
    /// when an item is picked up, EquipWeapon is called. A weapon is instantiated into the character's hands
    /// and we cache a reference to the class that inherits from weapon on this object. it is then told that is has
    /// been equipped and the animation controller is told of it's animation type.
    /// </summary>
    public void EquipWeapon(GameObject prefab)
    {
        equippedWeapon = Instantiate(prefab, attachmentPoint, false);
        //Get weapon offset from weapon itself
        currentWeapon = equippedWeapon.GetComponent<Weapon>();
        currentWeapon.Equipped = true;
        charAnimController.SetWeaponAnimationProperties((int) currentWeapon.animationType);
    }

    /// <summary>
    /// When a new weapon is picked up we need to get rid of the old one, to this destroys the current weapon and
    /// preps all the variables for reassignment
    /// </summary>
    public void UnequipWeapon()
    {
        if (!equippedWeapon) return;
        currentWeapon.Equipped = false;
        Destroy(equippedWeapon);
        equippedWeapon = null;
        currentWeapon = null;
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