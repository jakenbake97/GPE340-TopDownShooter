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
        target = FindObjectOfType<Player>();

        if (!target || target.Health.HealthValue <= 0f)
        {
            agent.isStopped = true;
            anim.SetFloat(Horizontal, 0f);
            anim.SetFloat(Vertical, 0f);
            return;
        }

        agent.SetDestination(target.transform.position);
        desiredVelocity = agent.desiredVelocity;
        Vector3 input = transform.InverseTransformDirection(desiredVelocity);
        anim.SetFloat(Horizontal, input.x);
        anim.SetFloat(Vertical, input.z);

        if (!(Vector3.Angle(transform.forward, target.transform.position) <= currentWeapon.attackAngle)) return;
        if (Vector3.SqrMagnitude(transform.position - target.transform.position) <=
            currentWeapon.maxRange * currentWeapon.maxRange)
        {
            currentWeapon.processShoot();
        }
    }

    private void OnAnimatorMove()
    {
        agent.velocity = anim.velocity;
    }
}