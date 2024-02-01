using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BulletMultiplyObject : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private BulletMultiply _bulletMultiply;
    private Animator _animator;


    private void Awake()
    {
        _rigidbody = FindObjectOfType<Rigidbody2D>();
        _bulletMultiply = FindObjectOfType<BulletMultiply>();
        _animator = GetComponent<Animator>();
    }

    void Start()
    {
        _rigidbody.velocity = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * _bulletMultiply.bulletForce;
        StartCoroutine(BulletBoom());
    }

    IEnumerator BulletBoom()
    {
        yield return new WaitForSeconds(_bulletMultiply.TimeBoom);
        if(_animator != null) _animator.SetBool("Boom",true);
        yield return new WaitForSeconds(0.4f);
        for (int i = 0; i < _bulletMultiply.CountBullet; i++)
        {
            var x = Mathf.Cos((360f * Mathf.Deg2Rad / _bulletMultiply.CountBullet) * i);
            var y = Mathf.Sin((360f * Mathf.Deg2Rad / _bulletMultiply.CountBullet) * i);
            Vector2 temp = new Vector2(x, y) + (Vector2)transform.position;
            Vector2 dir = temp - (Vector2)transform.position;
            GameObject bullet = Instantiate(_bulletMultiply.prefabBulletTwo, temp, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = dir.normalized * _bulletMultiply.bulletForce;
        }
        yield return new WaitForSeconds(0.01f);
        Destroy(gameObject);

    }

    
}
