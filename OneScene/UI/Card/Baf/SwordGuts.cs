using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordGuts : MonoBehaviour
{
    public GameObject prefabSword;
    public int Damage = 10;
    public float force;
    public float speedAngle;
    public float spawnTime;
    public bool isStart;

    private GameObject _player;
    private GameObject _cursor;
    private float _time;

    private void Awake()
    {
        _player = GameObject.FindWithTag("Player");
        _cursor = GameObject.FindWithTag("Cursor");
    }

    public void StartBaf()
    {
        isStart = true;
    }

    public void AddDamage()
    {
        Damage += 10;
    }
    
    public void LowKd()
    {
        spawnTime -= 1f;
    }
    
    private void Update()
    {
        if (isStart)
        {
            if (_time <= 0)
            {
                GameObject sword = Instantiate(prefabSword, _player.transform.position, Quaternion.identity);
                sword.GetComponent<Rigidbody2D>().velocity =
                    (_cursor.transform.position - _player.transform.position).normalized * force;
                _time = spawnTime;
            }
            else
            {
                _time -= Time.deltaTime;
            }
        }
    }
}
