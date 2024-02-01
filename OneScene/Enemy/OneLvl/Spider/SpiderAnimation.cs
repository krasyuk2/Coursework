using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAnimation : MonoBehaviour
{
    private Animator _animator;
    private Spider _spider;
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spider = GetComponent<Spider>();
        _rigidbody2D = GetComponent<Rigidbody2D>();


    }

    private void Update()
    {
        Anim();
    }

    void Anim()
    {
        if (_spider.isJump) 
        {
            _animator.SetBool(Animator.StringToHash("Jump"),true);
        }
        else
        {
            _animator.SetBool(Animator.StringToHash("Jump"),false);

        }
    }
}

