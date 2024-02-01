using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class ExperienceObject : MonoBehaviour
{
    private Experience _experience;
    private GameObject _player;
    private Statistics _statistics;
    public int ExpValue;
    public float Speed;

    
    private void Awake()
    {
        _experience = FindObjectOfType<Experience>();
        _player = GameObject.FindWithTag("Player");
        _statistics = FindObjectOfType<Statistics>();
        ExpValue = _experience.ExpValue;
        Speed = _experience.Speed;
   
    }

    private void Start()
    {

        Color startColor = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f), 1);
        GetComponent<TrailRenderer>().startColor = startColor;
        GetComponent<TrailRenderer>().endColor = startColor;
        GetComponent<SpriteRenderer>().material.color = startColor;
        GetComponent<TrailRenderer>().material.color = startColor;

    }

   

    void Update()
    {
        if (Vector2.Distance(transform.position, _player.transform.position) < _experience.Distance)
        {
            transform.position =
                Vector3.MoveTowards(transform.position, _player.transform.position, Time.deltaTime * Speed);
        }

        if (transform.position == _player.transform.position)
        {
            _statistics.PlayerExp += ExpValue;
            _statistics.AllPlayerExp += ExpValue;
            Destroy(gameObject);
        }
    }
}
