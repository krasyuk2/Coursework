using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Dash : MonoBehaviour
{
    private GameObject _player;
    private Rigidbody2D _rigidbody2D;
    private GameObject _cursor;
    public GameObject DashPrefab;
    public GameObject imageButtonPrefab;
    public float impulseDash;
    public float TimeRateDash = 10f;
    public float TimeDash;
    private bool _isStartDash;
    private GameObject _canvas;
    
    private void Awake()
    {
        _rigidbody2D = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
        _player = GameObject.FindWithTag("Player");
        _cursor = GameObject.FindWithTag("Cursor");
        _canvas = GameObject.FindWithTag("Canvas");

    }
    
    public void StartBaf()
    {
        _isStartDash = true;
    }

    private void Update()
    {
        if(_isStartDash) {
            if (TimeDash <= 0)
            {
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    if (!flag)
                    {
                        StartCoroutine(StartDash());
                    }

                }
            }
            else
            {
                TimeDash -= Time.deltaTime;
            }
        }

        if (TimeDash < 0) TimeDash = 0;
     
    }

    private float startSpeed;
    private bool flag;
    IEnumerator StartDash()
    {
        flag = true;
        startSpeed = _player.GetComponent<Move>().Speed;
        _player.GetComponent<Move>().Speed *= 2;
        yield return new WaitForSeconds(5f);
        _player.GetComponent<Move>().Speed = startSpeed;
        TimeDash = TimeRateDash;
        flag = false;

    }

    
}
