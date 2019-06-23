using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyTarget : MonoBehaviour
{
    private Transform bulletParent;

    private void OnCollisionEnter(Collision other)
    {
        bulletParent = other.transform.parent;
    }

    public void LookAtShooter()
    {
        transform.LookAt(bulletParent);
    }

    public void ParentEventRespawn()
    {
        GetComponentInParent<RespawnStationary>().EventRespawn();
    }
}