﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warlock : Enemy
{

    public GameObject fireBall;
    public float timeBetweenShots;
    float nextShotTime;
    public Transform shotPoint;
    private Transform target;
    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Update()
    {
        if (Vector2.Distance(transform.position, target.transform.position) <= 20f)
        {
            if (Time.time > nextShotTime)
            {
                //Instantiate(fireBall, shotPoint.position, shotPoint.rotation);
                Shoot();
                nextShotTime = Time.time + timeBetweenShots;
            }
        }
    }
    private void Shoot()
    {
        GameObject bullet = ObjectPoolManager.Instance.GetBullet();
        bullet.transform.position = shotPoint.transform.position;
    }
}
