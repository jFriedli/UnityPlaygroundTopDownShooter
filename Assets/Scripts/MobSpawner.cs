﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    public bool activated = true;

    public float spawnTime = 2f;
    public float spawnLift = 100f;

    public float rangeMin = 10f;
    public float rangeMax = 10f;

    public GameObject plane;
    public GameObject simpleMob;

    float timer;

    // Update is called once per frame
    void Update()
    {
        if (activated && spawnNext())
        {
            spawnSimpleMob();
        }
    }


    bool spawnNext()
    {
        timer += 1.0f * Time.deltaTime;
        if (timer >= spawnTime)
        {
            return true;
        }
        return false;
    }

    void spawnSimpleMob()
    {
        timer = 0f;

        Vector3 targetPos = plane.transform.position;
        targetPos += Vector3.up * spawnLift;
        targetPos += Vector3.right * Random.Range(-rangeMin, rangeMax);
        targetPos += Vector3.forward * Random.Range(-rangeMin, rangeMax);


        Instantiate(simpleMob.transform, targetPos, Quaternion.identity);
    }
}
