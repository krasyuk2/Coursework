using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimation : MonoBehaviour
{
    private Animator _animator;
    private Weapon _weapon;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _weapon = GetComponent<Weapon>();
      
    }
    
    private void Update()
    {
        AnimFire();
        AnimFire2();
    }

    void AnimFire2()
    {
        if (_weapon._mouseFire2Down)
        {
            _animator.SetBool(Animator.StringToHash("Fire2"),true);
            
        }
        else
        {
            _animator.SetBool(Animator.StringToHash("Fire2"),false);
        }
    }

    void AnimFire()
    {
        if (_weapon.fire)
        {
            _animator.SetBool(Animator.StringToHash("Fire"),true);
            _animator.speed = 2f;
        }
        else
        {
            _animator.SetBool(Animator.StringToHash("Fire"),false);
        }   
    }
}
