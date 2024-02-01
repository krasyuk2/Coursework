using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public GameObject swordPrefab;
    private GameObject _player;
    public float spawnRate;
    public int damage;
    private float timeSpawn;
    private bool isStart;
    private void Awake()
    {
        _player = GameObject.FindWithTag("Player");
    }

    public void StartSword()
    {
        isStart = true;
    }

    public void AddDamageSword()
    {
        damage += 10;
    }


    void Update()
    {
        if (isStart)
        {
            if (timeSpawn <= 0)
            {
                Instantiate(swordPrefab, _player.transform.position, quaternion.identity);
                timeSpawn = spawnRate;
            }
            else
            {
                timeSpawn -= Time.deltaTime;
            }
        }

    }
}
