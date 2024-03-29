﻿using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    private Rigidbody[] childRBs;
    private Collider[] childColliders;
    private Rigidbody mainRB;
    private Collider mainCollider;
    private Animator animator;

    private void Awake()
    {
        childRBs = GetComponentsInChildren<Rigidbody>();
        childColliders = GetComponentsInChildren<Collider>();
        mainRB = GetComponent<Rigidbody>();
        mainCollider = GetComponent<Collider>();
        animator = GetComponent<Animator>();
        ConvertToNormal();
    }

    /// <summary>
    /// switch the model to ragdoll mode by calculating physics on rigibodies and enabling colliders
    /// </summary>
    public void ConvertToRagdoll()
    {
        foreach (var rB in childRBs)
        {
            rB.isKinematic = false;
        }

        foreach (var coll in childColliders)
        {
            coll.enabled = true;
        }

        mainCollider.enabled = false;
        mainRB.isKinematic = true;
        animator.enabled = false;
    }

    /// <summary>
    /// switch the model to normal mode by disabling physics on rigidbodies and disabling colliders
    /// </summary>
    private void ConvertToNormal()
    {
        foreach (var rB in childRBs)
        {
            rB.isKinematic = true;
        }

        foreach (var coll in childColliders)
        {
            coll.enabled = false;
        }

        mainCollider.enabled = true;
        mainRB.isKinematic = false;
        animator.enabled = true;
    }
}