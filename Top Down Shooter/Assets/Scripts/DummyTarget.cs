using JetBrains.Annotations;
using UnityEngine;

public class DummyTarget : MonoBehaviour
{
    [SerializeField, Tooltip("The weapon the dummy will spawn with")]
    private GameObject weapon;

    [SerializeField, Tooltip("The position the weapon should spawn at")]
    private Transform weaponSpawnPoint;

    private Weapon currentWeapon;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        var spawnedWeapon = Instantiate(weapon, weaponSpawnPoint.position, Quaternion.identity);
        spawnedWeapon.transform.SetParent(weaponSpawnPoint, true);
        currentWeapon = spawnedWeapon.GetComponent<Weapon>();
    }

    /// <summary>
    /// Event called when the animator does an IK pass. If there is a currentWeapon, send its IK targets to the
    /// SetCharacterIKAnimation method
    /// </summary>
    private void OnAnimatorIK(int layerIndex)
    {
        if (!currentWeapon) return;
        SetCharacterIKAnimation(currentWeapon.rightHandIKTarget, currentWeapon.leftHandIKTarget,
            currentWeapon.rightElbowIKHint, currentWeapon.leftElbowIKHint);
    }

    /// <summary>
    /// Optionally takes 4 parameters for setting the IK targets and hints. Each target is then given a weight of 1 to
    /// follow the IK points as best as possible
    /// </summary>
    private void SetCharacterIKAnimation([CanBeNull] Transform rightHandTarget, [CanBeNull] Transform leftHandTarget,
                                         [CanBeNull] Transform rightElbowHint, [CanBeNull] Transform leftElbowHint)
    {
        if (rightHandTarget)
        {
            anim.SetIKPosition(AvatarIKGoal.RightHand, rightHandTarget.position);
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
            anim.SetIKRotation(AvatarIKGoal.RightHand, rightHandTarget.rotation);
            anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);
        }
        else
        {
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 0f);
            anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 0f);
        }

        if (leftHandTarget)
        {
            anim.SetIKPosition(AvatarIKGoal.LeftHand, leftHandTarget.position);
            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
            anim.SetIKRotation(AvatarIKGoal.LeftHand, leftHandTarget.rotation);
            anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1f);
        }
        else
        {
            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0f);
            anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0f);
        }

        if (rightElbowHint)
        {
            anim.SetIKHintPosition(AvatarIKHint.RightElbow, rightElbowHint.position);
            anim.SetIKHintPositionWeight(AvatarIKHint.RightElbow, 1f);
        }
        else
        {
            anim.SetIKHintPositionWeight(AvatarIKHint.RightElbow, 0f);
        }

        if (leftElbowHint)
        {
            anim.SetIKHintPosition(AvatarIKHint.LeftElbow, leftElbowHint.position);
            anim.SetIKHintPositionWeight(AvatarIKHint.LeftElbow, 1f);
        }
        else
        {
            anim.SetIKHintPositionWeight(AvatarIKHint.LeftElbow, 0f);
        }
    }
}