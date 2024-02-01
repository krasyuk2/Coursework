using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float BulletForce;
    public GameObject prefabCoin;
    private float TimeRate = 5;
    private GameObject _player;
    private float _time;
    public bool isStart;
    private void Awake()
    {
        _player = GameObject.FindWithTag("Player");
    }

    public void StartBaf()
    {
        isStart = true;
    }
    

    private void Update()
    {
        if (isStart)
        {
            if (_time <= 0)
            {
                Instantiate(prefabCoin, _player.transform.position, Quaternion.identity);
                _time = TimeRate;
            }
            else
            {
                _time -= Time.deltaTime;
            }
        }


    }
}
