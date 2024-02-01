using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D _rigidbody2D;
    private Move _move;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _move = GetComponent<Move>();
    }
    void Update()
    {
        Move();
        Dead();
        Invulnerability();
       
    }

    private float _timeRateCoffee = 15f;
    private float _timeCoffee = 7f;
    void Move()
    {
        if (_rigidbody2D.velocity != Vector2.zero)
        {
            _animator.SetBool(Animator.StringToHash("Move"),true);
            _animator.SetBool(Animator.StringToHash("Coffe"),false);
            _timeCoffee = _timeRateCoffee;
        }
        else
        {
            if (_timeCoffee <= 0)
            {
                _animator.SetBool(Animator.StringToHash("Coffe"),true);
                StartCoroutine(WaitFalseAnimationCoffee());
                _timeCoffee = _timeRateCoffee;
            }
            else
            {
                _timeCoffee -= Time.deltaTime;
            }
            _animator.SetBool(Animator.StringToHash("Move"),false);
        }

        if (Time.timeScale == 0) // чтобы стоял когла время остановилось
        {
            _animator.SetBool(Animator.StringToHash("Move"),false);
            
        }
    }

    void Dead()
    {
        if (_move.Heal <= 0)
        {
            _animator.SetBool(Animator.StringToHash("Dead"),true);
        }
    }

    void Invulnerability()
    {
        if (!_move.isDamage)
        {
            _animator.SetBool(Animator.StringToHash("Invul"),true);
        }
        else _animator.SetBool(Animator.StringToHash("Invul"),false);
    }

    IEnumerator WaitFalseAnimationCoffee()
    {
        yield return new WaitForSeconds(3.1f);
        _animator.SetBool(Animator.StringToHash("Coffe"),false);
    }

   
}
