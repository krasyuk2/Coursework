using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeadBomb : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        DeadAnimation();
    }
    
    private void DeadAnimation()
    {
        int countAnimation = Random.Range(0, 4);
        _animator.SetInteger(Animator.StringToHash("Dead"), countAnimation);
        StartCoroutine(DestroyObject());
    }
    IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

}
