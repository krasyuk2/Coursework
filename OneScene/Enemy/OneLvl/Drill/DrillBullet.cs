using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillBullet : MonoBehaviour
{
    private GameObject _player;
    private Rigidbody2D _rigidbody2D;
    private Vector2 dir;
    private Drill _drill;
    private Animator _animator;
    private void Awake()
    {
        _player = GameObject.FindWithTag("Player");
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _drill = FindObjectOfType<Drill>();
    }

    private void Start()
    {
        dir = _player.transform.position - transform.position;
        _rigidbody2D.velocity = dir.normalized * _drill.BulletForce;
        StartCoroutine(WaitDelete());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _player.GetComponent<Move>().TakeDamage(_drill.Damage);
            StartCoroutine(Delete());
        }
    }

    IEnumerator Delete()
    {
        _animator.SetBool(Animator.StringToHash("Boom"),true);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    IEnumerator WaitDelete()
    {
        yield return new WaitForSeconds(4f);
        StartCoroutine(Delete());
    }
}
