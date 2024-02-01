using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonState : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnMouseEnter()
    {
        _animator.SetBool(Animator.StringToHash("Start"),true);
    }

    private void OnMouseExit()
    {
        
        _animator.SetBool(Animator.StringToHash("Start"),false);
    }
}
