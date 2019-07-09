using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : WeaponAgent
{
    private NavMeshAgent agent;
    public Player target;

    private static readonly int Horizontal = Animator.StringToHash("Horizontal");
    private static readonly int Vertical = Animator.StringToHash("Vertical");
    private Vector3 desiredVelocity;
    [SerializeField] private GameObject[] weaponPrefabs;


    public override void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        base.EquipWeapon(weaponPrefabs[Random.Range(0, weaponPrefabs.Length)]);
        base.Awake();
        UIManager.Instance.RegisterEnemy(this);
    }

    private void Start()
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
    }

    // Update is called once per frame
    private void Update()
    {
        if (GameManager.Paused) return;
        if (!UpdateAgentForTarget()) return;

        DetermineShoot();
    }

    /// <summary>
    /// Looks for a target player, if one is found then updates the NavMeshAgent and the animator,
    /// if a player is not found then the agent stops and returns false preventing the agent from moving or shooting
    /// </summary>
    private bool UpdateAgentForTarget()
    {
        target = GameManager.Player;
        if (!GameManager.Player || GameManager.Player.Health.HealthValue <= 0f)
        {
            agent.isStopped = true;
            anim.SetFloat(Horizontal, 0f);
            anim.SetFloat(Vertical, 0f);
            return false;
        }

        agent.isStopped = false;
        agent.SetDestination(GameManager.Player.transform.position);
        desiredVelocity = agent.desiredVelocity;
        Vector3 input = transform.InverseTransformDirection(desiredVelocity);
        anim.SetFloat(Horizontal, input.x);
        anim.SetFloat(Vertical, input.z);
        return true;
    }

    /// <summary>
    /// Calculates the angle between the agent and the player to determine if it is point at the player, then
    /// calculates distance to determine if it is in range
    /// </summary>
    private void DetermineShoot()
    {
        var angle = Vector3.Angle(transform.forward, GameManager.Player.transform.position);
        if (!(angle <=
              currentWeapon.attackAngle)) return;
        if (Vector3.SqrMagnitude(transform.position - GameManager.Player.transform.position) <=
            currentWeapon.maxRange * currentWeapon.maxRange)
        {
            currentWeapon.processShoot();
        }
    }

    /// <summary>
    /// Sets the animator velocity to that of the agent's
    /// </summary>
    private void OnAnimatorMove()
    {
        agent.velocity = anim.velocity;
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (!equippedWeapon || !currentWeapon) return;
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