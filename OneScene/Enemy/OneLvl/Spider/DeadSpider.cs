using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeadSpider : MonoBehaviour
{
    private Material _color;
    private Color _startColor;
    private Animator _animator;
    
    private void Awake()
    {
        _color = GetComponent<SpriteRenderer>().material;
        _animator = GetComponent<Animator>();

    }

    private void Start()
    {
        DeadAnimation();
        _startColor = _color.color;
        StartCoroutine(Show());
        
    }

    private void Update()
    {
        if(_color.color.a <= 0) Destroy(gameObject);
    }

    IEnumerator Show()
    {
        for (float i = 1; i > -0.1f; i -= 0.1f)
        {
            _color.color = new Color(_startColor.r, _startColor.g, _startColor.b, i);
            yield return new WaitForSeconds(0.1f);
        }
    }

    void DeadAnimation()
    {
        int countAnimation = Random.Range(0, 4);
        _animator.SetInteger(Animator.StringToHash("Dead"),countAnimation);
    }

 
   
}
