using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFireCircle : MonoBehaviour
{
    private GameObject _player;
    public GameObject prefabBaf;
    public float TimeRate;
    private float _time;
    public int Damage;
    public bool isStart;
    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    public void StartBaf()
    {
        isStart = true;
    }

    void Update()
    {
        if (isStart)
        {
            if (_time <= 0)
            {
                Instantiate(prefabBaf, new Vector2(_player.transform.position.x, _player.transform.position.y - 0.5f),
                    Quaternion.identity);
                _time = TimeRate;
            }
            else
            {
                _time -= Time.deltaTime;
            }
        }
    }
}
