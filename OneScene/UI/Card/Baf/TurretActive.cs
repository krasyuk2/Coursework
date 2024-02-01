using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretActive : MonoBehaviour
{
    public float timeRateLive;
    public float timeRateAttack;

    public float bulletForce;

    public float distance;
    public int damage;
    public GameObject prefabTurret;
    public GameObject prefabBullet;
    public GameObject timerBaf;
    private GameObject _player;
    public bool isLvlOne;
    public bool piercing;
    

    public float TimeRateSpawn;
    private float _time;
    private void Awake()
    {
        _player = GameObject.FindWithTag("Player");
    }

    public void OneBaf()
    {
        isLvlOne = true;
    }

    public void TwoBaf()
    {
        piercing = true;
    }

    public void TreeBaf()
    {
        TimeRateSpawn = 5f;
        damage += 10;
        timeRateLive += 10f;
    }
    

    
    public (float,float) Baf()
    {
        if (_time <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Instantiate(prefabTurret, _player.transform.position, Quaternion.identity);
                _time = TimeRateSpawn;
            } 
        }
        else
        {
            _time -= Time.deltaTime;
        }

        return (_time, TimeRateSpawn);

    }
}
