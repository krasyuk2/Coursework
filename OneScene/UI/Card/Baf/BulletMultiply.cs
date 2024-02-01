using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class BulletMultiply : MonoBehaviour
{
    public GameObject prefabBullet;
    public GameObject prefabBulletTwo;
    public int CountBullet;
    public int Damage;
    public float bulletForce;
    public float spawnRate;
    public float TimeBoom;
    private float _time;
    private GameObject _player;
    public bool isStart;

    private void Awake()
    {
        _player = GameObject.FindWithTag("Player");
    }

    public void StartBaf()
    {
        isStart = true;
    }

    public void AddDamageAndAddCount()
    {
        Damage += 5;
        CountBullet += 10;
    }

    private void Update()
    {
        if (isStart)
        {
            if (_time <= 0)
            {
                Instantiate(prefabBullet, _player.transform.position, Quaternion.identity);
                _time = spawnRate;
            }
            else
            {
                _time -= Time.deltaTime;
            }
        }
    }
}
