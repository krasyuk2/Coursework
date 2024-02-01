using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoTutorial : MonoBehaviour
{
    private Animator _animator;
    private BoxCollider2D _boxCollider2D;
    public bool isDead;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            _animator.SetBool(Animator.StringToHash("Dead"),true);
            isDead = true;
            _boxCollider2D.enabled = false;
            StartCoroutine(Dead());
        }
    }

    IEnumerator Dead()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
