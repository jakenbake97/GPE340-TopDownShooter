﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SemiWeapons : Weapon
{
    [SerializeField] private float spreadAngle;

    private float shootTime = 0f;
    private float signedSpreadAngle;

    private void Start()
    {
        ammoLeft = ammo;
        signedSpreadAngle = spreadAngle / 2f;
    }

    private void Update()
    {
        if (reloading) return;
        ShootInput();
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartReload();
        }
    }

    private void ShootInput()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        if (!(Time.time >= shootTime)) return;
        if (ammoLeft > 0)
        {
            Shoot();
        }
        else
        {
            StartReload();
        }
    }

    private void StartReload()
    {
        StartCoroutine(Reload());
    }

    public override void Shoot()
    {
        var bullet = Instantiate(bulletPrefab, barrel.position,
            barrel.rotation * Quaternion.Euler(Random.Range(-signedSpreadAngle, signedSpreadAngle) * Vector3.up));
        bullet.GetComponent<Bullet>().Damage = damage;
        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * bulletForce);
        shootTime = Time.time + fireRate;
        ammoLeft--;
    }
}