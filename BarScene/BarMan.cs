using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BarMan : MonoBehaviour
{
    private Animator _animator;
    private Animator _animatorShadow;
    private float _timeSmoke;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void Start()
    {
        
    }

   
    void Update()
    {
        if (_timeSmoke <= 0)
        {
            StartCoroutine(Smoke());
            _timeSmoke = Random.Range(5f, 15f);
        }
        else
        {
            _timeSmoke -= Time.deltaTime;
        }
    }

    IEnumerator Smoke()
    {
        _animator.SetBool(Animator.StringToHash("Smoke"),true);
        yield return new WaitForSeconds(0.2f);
        _animator.SetBool(Animator.StringToHash("Smoke"),false);
   
    }
}
