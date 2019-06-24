using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSight : MonoBehaviour
{
    private Weapon weaponRef;
    private LineRenderer line;

    private void Start()
    {
        weaponRef = GetComponent<Weapon>();
        line = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (weaponRef.Equipped)
        {
            line.enabled = true;
            line.SetPosition(0, weaponRef.barrel.position);
            RaycastHit hit;
            if (Physics.Raycast(weaponRef.barrel.position, transform.forward, out hit, Mathf.Infinity))
            {
                line.SetPosition(1, hit.point);
            }
        }
    }
}