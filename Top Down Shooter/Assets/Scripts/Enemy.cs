using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : WeaponAgent
{
    private NavMeshAgent agent;


    private Player target;
    private static readonly int Horizontal = Animator.StringToHash("Horizontal");
    private static readonly int Vertical = Animator.StringToHash("Vertical");
    private Vector3 desiredVelocity;


    // Start is called before the first frame update
    public override void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        base.Awake();
    }

    // Update is called once per frame
    private void Update()
    {
        target = FindObjectOfType<Player>();
        if (!target)
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
    }

    private void OnAnimatorMove()
    {
        agent.velocity = anim.velocity;
    }
}