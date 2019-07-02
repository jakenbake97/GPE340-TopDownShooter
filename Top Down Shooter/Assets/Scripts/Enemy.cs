using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : WeaponAgent
{
    private NavMeshAgent agent;


    private Player target;
    private static readonly int Horizontal = Animator.StringToHash("Horizontal");
    private static readonly int Vertical = Animator.StringToHash("Vertical");
    private Vector3 desiredVelocity;
    [SerializeField] private GameObject[] weaponPrefabs;


    public override void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        base.EquipWeapon(weaponPrefabs[Random.Range(0, weaponPrefabs.Length)]);
        base.Awake();
    }

    private void Start()
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!UpdateAgentForTarget()) return;

        DetermineShoot();
    }

    /// <summary>
    /// Looks for a target player, if one is found then updates the NavMeshAgent and the animator,
    /// if a player is not found then the agent stops and returns false preventing the agent from moving or shooting
    /// </summary>
    private bool UpdateAgentForTarget()
    {
        target = FindObjectOfType<Player>();

        if (!target || target.Health.HealthValue <= 0f)
        {
            agent.isStopped = true;
            anim.SetFloat(Horizontal, 0f);
            anim.SetFloat(Vertical, 0f);
            return false;
        }

        agent.SetDestination(target.transform.position);
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
        if (!(Vector3.Angle(transform.forward, target.transform.position) <= currentWeapon.attackAngle)) return;
        if (Vector3.SqrMagnitude(transform.position - target.transform.position) <=
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
}