using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(Animator))]
public class Enemy : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator anim;


    private Player target;
    private static readonly int Horizontal = Animator.StringToHash("Horizontal");
    private static readonly int Vertical = Animator.StringToHash("Vertical");
    private Vector3 desiredVelocity;


    // Start is called before the first frame update
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
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